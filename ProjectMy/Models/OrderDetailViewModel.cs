using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class OrderDetailViewModel
    {
        public bool OrderDetailsId { get; set; }

        public string ItemName { get; set; }

        public decimal ItemQty { get; set; }

        public decimal ItemTotalPrice { get; set; }
    }
}