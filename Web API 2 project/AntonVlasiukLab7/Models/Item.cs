using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AntonVlasiukLab7.Models
{
    public class Item : ModelBase
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
    }
}