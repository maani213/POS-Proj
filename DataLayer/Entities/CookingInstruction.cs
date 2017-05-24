using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class CookingInstruction
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string BackgroundColor { get; set; }
        public string TextStyle { get; set; }
        public string TextColor { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        [MaxLength(5)]
        public string FontSize { get; set; }
        public int PositionNumber { get; set; }
    }
}
