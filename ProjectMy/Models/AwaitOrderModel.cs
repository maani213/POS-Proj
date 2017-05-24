using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class AwaitOrderModel
    {
        public int AwaitOrderId { get; set; }

        public int OrderNo { get; set; }

        public string Date { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public string Collect { get; set; }

    }
}