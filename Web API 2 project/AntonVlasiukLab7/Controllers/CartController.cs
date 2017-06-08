using AntonVlasiukLab7.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AntonVlasiukLab7.Controllers
{
    [RoutePrefix("api/carts")]
    public class CartController : ApiController
    {
        [HttpGet, Route("")]
        [ResponseType(typeof(IEnumerable<Cart>))]
        public IHttpActionResult Get()
        {
            IEnumerable<Cart> carts;

            using (var ctx = new ShopContext())
            {
                carts = ctx.Carts.Include(c => c.Items).ToList();
            }

            return Ok(carts);
        }

        [HttpGet, Route("{id:int}", Name = "GetCart")]
        [ResponseType(typeof(Cart))]
        public IHttpActionResult Get(int id)
        {
            Cart cart;
            using (var ctx = new ShopContext())
            {
                cart = ctx.Carts.Include(c => c.Items)
                    .SingleOrDefault(c => c.Id == id);
            }

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpPost, Route("")]
        public IHttpActionResult Add([FromBody]Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var ctx = new ShopContext())
            {
                ctx.Carts.Add(cart);
                ctx.SaveChanges();
            }

            return CreatedAtRoute("GetCart", new { id = cart.Id }, cart);
        }

        [HttpPut, Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody]Cart cart)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var ctx = new ShopContext())
            {
                cart.Id = id;
                ctx.Carts.Attach(cart);
                var dbEntry = ctx.Entry(cart);
                dbEntry.State = EntityState.Modified;
                if (cart.Items != null)
                {
                    foreach (var item in cart.Items)
                    {
                        ctx.Items.Attach(item);
                        var entry = ctx.Entry(item);
                        entry.State = item.Id == 0 ? EntityState.Added :
                            EntityState.Modified;
                    }
                }

                ctx.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete, Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            using (var ctx = new ShopContext())
            {

                var dbEntry = ctx.Carts.Include(s => s.Items).FirstOrDefault(c => c.Id == id);

                if (dbEntry == null)
                {
                    return NotFound();
                }

                var itemsForDelete = dbEntry.Items.ToList();
                foreach (var item in itemsForDelete)
                {
                    ctx.Items.Remove(item);
                }

                ctx.Carts.Remove(dbEntry);
                ctx.SaveChanges();
            }
            return Ok();
        }

    }
}
