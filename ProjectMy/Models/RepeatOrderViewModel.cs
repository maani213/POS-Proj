using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class RepeatOrderViewModel
    {
        public List<OrderDetail> OrderDetails { get; set; }
        public decimal Total { get; set; }
    }

}