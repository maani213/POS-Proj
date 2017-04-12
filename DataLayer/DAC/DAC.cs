using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAC
{
    public class DAC
    {
        myContext db = new myContext();

        public void AddItem(Pizza item)
        {
            db.Pizzas.Add(item);
        }

        public List<Pizza> GetAllPizzas()
        {
            var result = db.Pizzas.ToList();
            if(result!=null)
            {
                return result;
            }
            else
            {
                return new List<Pizza>();
            }
        }
    }
}
