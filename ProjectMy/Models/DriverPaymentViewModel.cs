using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class DriverPaymentViewModel
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public int Trips { get; set; }
        public decimal Total { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }
    }
}