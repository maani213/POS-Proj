using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class PizzaModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string BackgroundColor { get; set; }
        public string TextStyle { get; set; }
        public int Size { get; set; }


    }
}