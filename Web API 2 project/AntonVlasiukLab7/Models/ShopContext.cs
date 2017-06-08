using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AntonVlasiukLab7.Models
{
    public class ShopContext : DbContext
    {
        public virtual IDbSet<Item> Items { get; set; }
        public virtual IDbSet<Cart> Carts { get; set; }

        public ShopContext() : base("MyNewShop") { }
    }
}