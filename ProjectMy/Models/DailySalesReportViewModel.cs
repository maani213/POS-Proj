using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class DailySalesReportViewModel
    {
        public string Date { get; set; }
        public string Day { get; set; }

        [Display(Name = "Total Sale")]
        public decimal TotalSale { get; set; }
    }
}