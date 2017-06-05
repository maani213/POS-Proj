using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class DeliverytoDespatch
    {
        [Key]
        public int Id { get; set; }

        public int AssignedDriverId { get; set; }

        public bool DriverPaymentStatus { get; set; }

        public bool DeliveryStatus { get; set; }

        public int OrderId { get; set; }
    }
}
