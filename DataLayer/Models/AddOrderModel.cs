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
        public Orders order { get; set; }
        public OrderDetails orderDetail { get; set; }
    }
}
