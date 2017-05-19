using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class OrderType
    {
        [Key]
        public int OrderTypeId { get; set; }
        public string OrderTypeName { get; set; }
    }
}
