using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class MonthlySalesViewModel
    {
        [Display(Name ="Month Name")]
        public string MonthName { get; set; }

        [Display(Name ="Total sale")]
        public decimal TotalSale { get; set; }
    }
}