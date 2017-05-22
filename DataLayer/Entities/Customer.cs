using DataLayer.Models;
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
    public class Customer //: Person
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(18)]
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [MaxLength(18)]
        [DisplayName("Sur Name")]
        public string SurName { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(18)]
        [Required]
        public string Phone { get; set; }

        [Index(IsUnique = true)]
        [MaxLength(18)]
        public string Mobile { get; set; }

        public string Email { get; set; }

        [Required]
        public string Address1 { get; set; }

        [Required]
        public string Address2 { get; set; }

        [MaxLength(8)]
        [DisplayName("Post Code")]
        public string PostCode { get; set; }

        //[DisplayName("AZ Sector")]
        //public string AZ_Sector { get; set; }

        public int Distance { get; set; } = 0;

        //[DisplayName("Confidential Comments")]
        //public string ConfidentialComments { get; set; }

        //[DisplayName("Customer Comments")]
        //public string CustomerComments { get; set; }

        [DisplayName("Driver Instructions ")]
        public string DriverInstructions { get; set; }

        //[Index(IsUnique = true)]
        //[DisplayName("Loyalty Card No")]
        //public int LoyaltyCardNo { get; set; } = 0;

        [DisplayName("Loyalty Points")]
        public int LoyaltyPoints { get; set; } = 0;

    }
}
