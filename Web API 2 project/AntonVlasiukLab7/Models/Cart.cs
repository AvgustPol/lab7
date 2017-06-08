using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntonVlasiukLab7.Models
{
    public class Cart : ModelBase
    {
        public string Name { get; set; }
        public IList<Item> Items { get; set; }
    }
}