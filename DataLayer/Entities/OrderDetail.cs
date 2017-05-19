using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailsId { get; set; }

        public string ItemName { get; set; }

        public int ItemQty { get; set; }

        public decimal Amount { get; set; }

        public int OrderId { get; set; }
    }
}
