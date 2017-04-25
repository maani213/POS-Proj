using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class ExtrasPriceViewModel
    {
        public string CategoryName { get; set; }
        public List<Sizes> sizes { get; set; }
        public List<ExtrasAndToppings> Items { get; set; }
    }
}