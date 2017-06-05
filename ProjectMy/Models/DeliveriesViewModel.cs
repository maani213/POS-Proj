using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class DeliveriesViewModel
    {
        public int DeliveryId { get; set; }

        public int OrderNo { get; set; }

        public string Time { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        [DisplayName("Post Code")]
        public string PostCode { get; set; }

        public string Telephone { get; set; }

        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; }
    }
}