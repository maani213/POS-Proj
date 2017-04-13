using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public int OrderDetailsId { get; set; }
    }
}
