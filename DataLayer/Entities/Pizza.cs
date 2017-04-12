using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Pizza
    {
        [Key]
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string BackgroundColor { get; set; }
        public string TextStyle { get; set; }
        public int Size { get; set; }

    }
}
