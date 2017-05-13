using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Toppings { get; set; }
        public string Title { get; set; }
        public string BackgroundColor { get; set; }
        public string TextStyle { get; set; }
        public string TextColor { get; set; }
        public string fontWeight { get; set; }
        public string fontStyle { get; set; }
        public int PositionNumber { get; set; }
    }
}