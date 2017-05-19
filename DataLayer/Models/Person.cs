using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Person
    {
        [MaxLength(18)]
        [Required]
        public string Name { get; set; }
        [MaxLength(18)]
        [Required]
        public string Phone { get; set; }
        [MaxLength(18)]
        public string Mobile { get; set; }
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

    }
}
