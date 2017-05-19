using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class ExtrasPriceViewModel
    {
        public string CategoryName { get; set; }
        public List<Size> sizes { get; set; }
        public List<ExtrasAndTopping> Items { get; set; }
    }
}