﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Item : Properties
    {
        [Key]
        public int Id { get; set; }
        public string Toppings { get; set; }

        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }

        public int CategoryId { get; set; }
    }
}
