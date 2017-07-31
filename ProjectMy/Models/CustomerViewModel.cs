using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class CustomerViewModel
    {
        public static Customer customer { get; set; }
        public static int OrderType { get; set; }
        static CustomerViewModel() {
            customer = null;
            OrderType = 0;
        }
    }
}
