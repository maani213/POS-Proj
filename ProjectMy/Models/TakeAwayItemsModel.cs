using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class TakeAwayItemsModel
    {
        public List<Size> sizes { get; set; }
        
        public List<Item> items { get; set; }

        public List<ExtrasAndTopping> Extras { get; set; }

    }
}