using DataLayer.Entities;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DAC
{
    public class DAC
    {
        static myContext db = new myContext();

        public static decimal GetItemPrice(int id, int sizeId)
        {
            decimal price = 0;
            if (sizeId == 1)
            {
                price = db.Items.SingleOrDefault(m => m.Id == id).Price1;
            }
            else if (sizeId == 2)
            {
                price = db.Items.SingleOrDefault(m => m.Id == id).Price2;
            }
            else if (sizeId == 3)
            {
                price = db.Items.SingleOrDefault(m => m.Id == id).Price3;
            }

            return price;
        }

        public static decimal GetExtrasPrices(List<int> id, int sizeId)
        {
            decimal price = 0;
            if (sizeId == 1)
            {
                price = db.Extras.Where(m => id.Contains(m.Id)).Sum(m => m.Price1);
                //price = db.Items.SingleOrDefault(m => m.Id == id).Price1;
            }
            else if (sizeId == 2)
            {
                price = db.Extras.Where(m => id.Contains(m.Id)).Sum(m => m.Price2);
            }
            else if (sizeId == 3)
            {
                price = db.Extras.Where(m => id.Contains(m.Id)).Sum(m => m.Price3);
            }

            return price;
        }
        public static void AddItem(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
        }

        public static List<Categories> GetAllCategories()
        {
            var categories = db.Categories.ToList();
            if (categories != null)
                return categories;
            else
                return new List<Categories>();
        }
        public static void AddCategory(Categories category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }
        public static void DeletePizza(int? id)
        {
            if (id != null)
            {
            }
        }

        public static Categories GetCategoryById(int? id)
        {
            var result = db.Categories.FirstOrDefault(m => m.Id == id);
            if (result != null)
            {
                return result;
            }
            else
                return new Categories();
        }

        public static List<Item> GetItemsByCategoryId(int? id)
        {
            var result = db.Items.Where(m => m.CategoryId == id).ToList();
            if (result != null)
            {
                return result;
            }
            else
                return new List<Item>();
        }

        public static List<Sizes> GetSizesByCategoryId(int id)
        {
            var result = (from pr in db.Sizes
                          where pr.CategoryId == id
                          orderby pr.Id
                          select pr
                          ).ToList();
            if (result != null)
            {
                return result;
            }
            else
                return new List<Sizes>();
        }
        public static List<string> GetSizesNameByCatId(int id)
        {
            var result = (from s in db.Sizes
                          where s.CategoryId == id
                          orderby s.Id
                          select s.Title).ToList();
            //db.Sizes.Where(m => m.CategoryId == id).ToList();
            if (result != null)
            {
                return result;
            }
            else
                return new List<string>();
        }

        //public static void AddPrice(Prices price)
        //{
        //    db.Prices.Add(price);
        //    db.SaveChanges();
        //}

        public static void AddSize(Sizes item)
        {
            db.Sizes.Add(item);
            db.SaveChanges();
        }
        public static void DeleteItemById(int? id)
        {
            var item = db.Items.First(m => m.Id == id);
            db.Items.Remove(item);
            db.SaveChanges();
        }
        public static void UpdatedPrices(Item itemPrices)
        {
            Item Updated = db.Items.Find(itemPrices.Id);
            Updated.Price1 = itemPrices.Price1;
            Updated.Price2 = itemPrices.Price2;
            Updated.Price3 = itemPrices.Price3;

            db.Entry(Updated).State = EntityState.Modified;
            db.SaveChanges();
        }

        public static void UpdateExtrasPrices(ExtrasAndToppings itemPrices)
        {
            ExtrasAndToppings Updated = db.Extras.Find(itemPrices.Id);
            Updated.Price1 = itemPrices.Price1;
            Updated.Price2 = itemPrices.Price2;
            Updated.Price3 = itemPrices.Price3;

            db.Entry(Updated).State = EntityState.Modified;
            db.SaveChanges();
        }
        //public static List<Prices> GetPricesCategorywise(int categoryId)
        //{
        //    var result = (from itm in db.Items
        //                  join pr in db.Prices on itm.Id equals pr.ItemId
        //                  where itm.CategoryId == categoryId
        //                  select pr).ToList();
        //    return result;
        //}

        //public static List<Prices> GetPriceByItemId(int ItemId)
        //{
        //    var prices = (from pr in db.Prices
        //                  where pr.ItemId == ItemId
        //                  orderby pr.SizeId
        //                  select pr
        //                  ).ToList();
        //    return prices;
        //}

        //public static List<Pizza> GetAllPizzas()
        //{
        //    var result = db.Pizzas.ToList();
        //    if (result != null)
        //    {
        //        return result;
        //    }
        //    else
        //    {
        //        return new List<Pizza>();
        //    }
        //}
        public static List<ExtrasAndToppings> GetExtrasByCategoryId(int categoryId)
        {
            var result = db.Extras.Where(m => m.CategoryId == categoryId).ToList();
            return result;
        }
        public static List<ExtrasAndToppings> GetAllExtras()
        {
            var result = db.Extras.ToList();
            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<ExtrasAndToppings>();
            }
        }
        public static void AddOrder(AddOrderModel orderDetails)
        {
            db.Orders.Add(orderDetails.order);
            db.OrderDetails.Add(orderDetails.orderDetail);
            db.SaveChanges();
        }
        public static void AddExtra(ExtrasAndToppings extra)
        {
            db.Extras.Add(extra);
            db.SaveChanges();
        }
    }
}
