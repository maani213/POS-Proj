using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalPaid { get; set; }

        public decimal Discount { get; set; }

        public decimal Balance { get; set; }

        [MaxLength(20)]
        public string OrderTypeName { get; set; }

        public int PaymentTypeId { get; set; }

        public int CustomerId { get; set; }

    }
}
