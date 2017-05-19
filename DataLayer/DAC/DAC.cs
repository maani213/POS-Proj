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
                price = db.ExtrasAndToppings.Where(m => id.Contains(m.Id)).Sum(m => m.Price1);
                //price = db.Items.SingleOrDefault(m => m.Id == id).Price1;
            }
            else if (sizeId == 2)
            {
                price = db.ExtrasAndToppings.Where(m => id.Contains(m.Id)).Sum(m => m.Price2);
            }
            else if (sizeId == 3)
            {
                price = db.ExtrasAndToppings.Where(m => id.Contains(m.Id)).Sum(m => m.Price3);
            }

            return price;
        }
        public static Item AddItem(Item item)
        {
            var Newitem = db.Items.Add(item);
            db.SaveChanges();
            return Newitem;
        }

        public static List<Category> GetAllCategories()
        {
            List<Category> Category = new List<Category>();

            using (myContext db1 = new myContext())
            {
                Category = db1.Categories.ToList();
            }
            if (Category != null)
                return Category;
            else
                return new List<Category>();
        }
        public static void AddCategory(Category category)
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

        public static Category GetCategoryById(int? id)
        {
            var result = db.Categories.FirstOrDefault(m => m.Id == id);
            if (result != null)
            {
                return result;
            }
            else
                return new Category();
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

        public static List<Size> GetSizesByCategoryId(int id)
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
                return new List<Size>();
        }
        public static List<string> GetSizeNameByCatId(int id)
        {
            var result = (from s in db.Sizes
                          where s.CategoryId == id
                          orderby s.Id
                          select s.Title).ToList();
            //db.Size.Where(m => m.CategoryId == id).ToList();
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

        public static Size AddSize(Size item)
        {
            var result = db.Sizes.Add(item);
            db.SaveChanges();
            return result;
        }
        public static void DeleteItemById(int? id)
        {
            var item = db.Items.First(m => m.Id == id);
            if (item != null)
            {
                db.Items.Remove(item);
                db.SaveChanges();
            }
        }

        public static void DeleteMultipleItems(int[] ids)
        {
            foreach (var id in ids)
            {
                var item = db.Items.First(m => m.Id == id);
                if (item != null)
                {
                    db.Items.Remove(item);
                }
            }
            db.SaveChanges();
        }

        public static void DeleteSizeById(int? id)
        {
            var item = db.Sizes.First(m => m.Id == id);
            if (item != null)
            {
                db.Sizes.Remove(item);
                db.SaveChanges();
            }
        }


        public static void DeleteExtraById(int? id)
        {
            var item = db.ExtrasAndToppings.First(m => m.Id == id);
            if (item != null)
            {
                db.ExtrasAndToppings.Remove(item);
                db.SaveChanges();
            }
        }

        public static void DeleteCategoryItemById(int? id)
        {
            var item = db.Categories.First(m => m.Id == id);
            if (item != null)
            {
                db.Categories.Remove(item);
                db.SaveChanges();
            }
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

        public static void UpdateExtrasPrices(ExtrasAndTopping itemPrices)
        {
            ExtrasAndTopping Updated = db.ExtrasAndToppings.Find(itemPrices.Id);
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
        public static List<ExtrasAndTopping> GetExtrasByCategoryId(int categoryId)
        {
            var result = db.ExtrasAndToppings.Where(m => m.CategoryId == categoryId).ToList();
            return result;
        }
        public static List<ExtrasAndTopping> GetAllExtras()
        {
            int catId = (from c in db.Categories
                         where c.Title.ToLower().Contains("pizza")
                         select c.Id).Single();
            var result = db.ExtrasAndToppings.Where(m => m.CategoryId != catId).ToList();
            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<ExtrasAndTopping>();
            }
        }

        public static List<ExtrasAndTopping> GetAllToppings()
        {
            int catId = (from c in db.Categories
                         where c.Title.ToLower().Contains("pizza")
                         select c.Id).Single();
            var result = db.ExtrasAndToppings.Where(m => m.CategoryId == catId).ToList();

            if (result != null)
            {
                return result;
            }
            else
            {
                return new List<ExtrasAndTopping>();
            }
        }

        public static Order AddOrder(Order NewOrder)
        {
            Order order = db.Orders.Add(NewOrder);
            db.SaveChanges();
            return order;
        }

        public static void AddOrderDetails(OrderDetail NewOrder)
        {
            var order = db.OrderDetails.Add(NewOrder);
            db.SaveChanges();
        }

        public static ExtrasAndTopping AddExtra(ExtrasAndTopping extra)
        {
            var result = db.ExtrasAndToppings.Add(extra);
            db.SaveChanges();
            return result;
        }

        public static Item UpdateItem(Item UpdatedItem)
        {
            var item = db.Items.FirstOrDefault(m => m.Id == UpdatedItem.Id);

            if (item != null)
            {
                item.Title = UpdatedItem.Title;
                item.BackgroundColor = UpdatedItem.BackgroundColor;
                item.TextColor = UpdatedItem.TextColor;
                item.Toppings = UpdatedItem.Toppings;
                item.IsBold = UpdatedItem.IsBold;
                item.IsItalic = UpdatedItem.IsItalic;
                item.FontSize = UpdatedItem.FontSize;
                item.TextStyle = UpdatedItem.TextStyle;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return item;
        }

        public static void UpdateSize(Size UpdatedItem)
        {
            var item = db.Sizes.FirstOrDefault(m => m.Id == UpdatedItem.Id);

            if (item != null)
            {
                item.Title = UpdatedItem.Title;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void UpdateExtrasItem(ExtrasAndTopping UpdatedItem)
        {
            var item = db.ExtrasAndToppings.FirstOrDefault(m => m.Id == UpdatedItem.Id);

            if (item != null)
            {
                item.Title = UpdatedItem.Title;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void UpdateCategory(Category UpdatedItem)
        {
            var item = db.Categories.FirstOrDefault(m => m.Id == UpdatedItem.Id);

            if (item != null)
            {
                item.Title = UpdatedItem.Title;
                item.BackgroundColor = UpdatedItem.BackgroundColor;
                item.TextColor = UpdatedItem.TextColor;

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static int AddCustomerIfNotExist(Customer customer)
        {
            var cust = db.Customers.FirstOrDefault(m => m.Phone == customer.Phone || m.Mobile == customer.Mobile);
            if (cust == null)
            {
                using (myContext db1 = new DataLayer.myContext())
                {
                    var NewCustomer = db1.Customers.Add(customer);
                    db1.SaveChanges();
                    return NewCustomer.Id;
                }
            }
            else
            {
                return cust.Id;
            }
        }

        public static Customer FindCustomer(string type, string value)
        {
            using (myContext db = new myContext())
            {
                var customer = db.Customers.FirstOrDefault(m => m.Phone == value);
                return customer;
            }
        }

        public static List<Order> GetCustomerOrders(int CustomerId)
        {
            using (myContext db1 = new myContext())
            {
                var CustomerOrder = (from order in db1.Orders
                                     where order.CustomerId == CustomerId
                                     orderby order.Date
                                     select order).ToList();
                return CustomerOrder;
            }
        }

        public static List<OrderDetail> GetOrderDetails(int OrderId)
        {
            using (myContext db1 = new myContext())
            {
                var OrderDetails = (from orderdetail in db1.OrderDetails
                                    where orderdetail.OrderId == OrderId
                                    select orderdetail
                                    ).ToList();
                return OrderDetails;
            }
        }
    }
}
