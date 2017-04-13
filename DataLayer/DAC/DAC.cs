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
           var res = db.Pizzas.Add(item);
            db.SaveChanges();
        }

        public void DeletePizza(int? id)
        {
            if(id!=null)
            {
                var result = db.Pizzas.Find(id);
                db.Pizzas.Remove(result);
                db.SaveChanges();
            }
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
