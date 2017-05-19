using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class AddOrderModel
    {
        public Order order { get; set; }
        public OrderDetail orderDetail { get; set; }
    }
}
