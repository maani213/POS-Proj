using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(18)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [MaxLength(8)]
        [DisplayName("Post Code")]
        public string PostCode { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(18)]
        [Required]
        [DisplayName("Tel / Mob")]
        public string Contact { get; set; }

        [DisplayName("Date Since")]
        public DateTime DateSince { get; set; }

        [DisplayName("Pay Rate")]
        [Required]
        public string PayRate { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName("Confirm Pin")]
        public string ConfirmPin { get; set; }

        public bool IngnorePin { get; set; }

        [Required]
        public bool Driver { get; set; }

    }
}
