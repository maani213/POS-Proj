using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }

        public string ItemName { get; set; }

        public decimal ItemQty { get; set; }

        public decimal ItemTotalPrice { get; set; }
    }
}
