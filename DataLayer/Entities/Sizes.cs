﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Sizes
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
}
