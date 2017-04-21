using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class PriceModel
    {
        public int ItemId { get; set; }
        public decimal price1 { get; set; }
        public decimal price2 { get; set; }
        public decimal price3 { get; set; }

    }
}