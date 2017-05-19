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
        public static string OrderType { get; set; }
        static CustomerViewModel() {
            customer = null;
            OrderType = null;
        }
    }
}
