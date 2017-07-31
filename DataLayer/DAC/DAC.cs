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
        public static string GetCategoryNameById(int id)
        {
            var result = db.Categories.FirstOrDefault(m => m.Id == id).Title;
            if (result != null)
            {
                return result;
            }
            else
                return "";
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

        #region Customer

        public static int AddOrUpdateCustomer(Customer customer)
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
                cust.Address1 = customer.Address1;
                cust.Address2 = customer.Address2;
                cust.FirstName = customer.FirstName;
                cust.SurName = customer.SurName;
                cust.Mobile = customer.Mobile;
                cust.Phone = customer.Phone;
                cust.PostCode = customer.PostCode;
                cust.LoyaltyPoints = customer.LoyaltyPoints;
                cust.Distance = customer.Distance;
                cust.Email = customer.Email;
                cust.DriverInstructions = customer.DriverInstructions;
                db.Entry(cust).State = EntityState.Modified;
                return cust.Id;
            }
        }

        public static Customer FindCustomerByPhone(string type, string value)
        {
            using (myContext db = new myContext())
            {
                var customer = db.Customers.FirstOrDefault(m => m.Phone == value);
                return customer;
            }
        }

        public static Customer FindCustomerById(int Id)
        {
            using (myContext db = new myContext())
            {
                var customer = db.Customers.FirstOrDefault(m => m.Id == Id);
                return customer;
            }
        }
        #endregion


        #region Orders

        public static Order AddOrder(Order NewOrder)
        {
            Order order = db.Orders.Add(NewOrder);
            db.SaveChanges();
            return order;
        }

        public static List<Order> GetAllOrdersDesc()
        {
            using (myContext db1 = new myContext())
            {
                var Orders = db1.Orders.OrderByDescending(m => m.OrderId).ToList();
                return Orders;
            }
        }

        public static void AddOrderDetails(OrderDetail NewOrder)
        {
            var order = db.OrderDetails.Add(NewOrder);
            db.SaveChanges();
        }

        public static Order GetCustomerOrders(int CustomerId)
        {
            using (myContext db1 = new myContext())
            {
                var CustomerOrder = (from order in db1.Orders
                                     where order.CustomerId == CustomerId
                                     orderby order.OrderId descending
                                     select order).FirstOrDefault();// ToList();
                return CustomerOrder;
            }
        }

        public static Order GetOrder(int OrderId)
        {
            using (myContext db1 = new myContext())
            {
                var Order = db1.Orders.FirstOrDefault(order => order.OrderId == OrderId);
                return Order;
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
        public static decimal GetOrderTotal(int id)
        {
            return db.Orders.Single(m => m.OrderId == id).TotalAmount;
        }

        public static decimal GetOrderPaymentStatus(int id)
        {
            var order = db.Orders.First(m => m.OrderId == id);
            if (order.Status == 1)
            {
                return order.TotalAmount;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        public static bool DeleteAllItemsByCategory(int catId)
        {
            List<Item> items = new List<Item>();
            using (myContext db1 = new myContext())
            {
                db1.Items.RemoveRange(db1.Items.Where(m => m.CategoryId == catId));
                db1.SaveChanges();
            }
            return true;
            //if (items != null)
            //{
            //    using (myContext db1 = new myContext())
            //    {
            //        db1.Items.RemoveRange(db1.Items.Where(m => m.CategoryId == catId));
            //        db1.SaveChanges();
            //    }
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        public static List<Discount> GetAllDiscounts()
        {
            using (myContext db1 = new myContext())
            {
                return db1.Discounts.ToList();
            }
        }


        #region Cooking Instructions

        public static List<CookingInstruction> GetAllCookInst()
        {
            using (myContext db1 = new myContext())
            {
                return db1.CookingInstructions.ToList();
            }
        }

        public static CookingInstruction AddCookInstr(CookingInstruction instr)
        {
            using (myContext db1 = new myContext())
            {
                var newItem = db1.CookingInstructions.Add(instr);
                db1.SaveChanges();
                return newItem;
            }
        }

        public static CookingInstruction UpdateCookingInstr(CookingInstruction UpdatedItem)
        {
            var item = db.CookingInstructions.FirstOrDefault(m => m.Id == UpdatedItem.Id);

            if (item != null)
            {
                item.Title = UpdatedItem.Title;
                item.BackgroundColor = UpdatedItem.BackgroundColor;
                item.TextColor = UpdatedItem.TextColor;
                item.IsBold = UpdatedItem.IsBold;
                item.IsItalic = UpdatedItem.IsItalic;
                item.FontSize = UpdatedItem.FontSize;
                item.TextStyle = UpdatedItem.TextStyle;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return item;
        }

        public static bool DeleteCookingInstr(int? id)
        {
            var item = db.CookingInstructions.First(m => m.Id == id);
            if (item != null)
            {
                db.CookingInstructions.Remove(item);
                db.SaveChanges();
            }
            return true;
        }
        #endregion



        #region Await Collection

        public static List<AwaitOrder> GetCollectionOrders()
        {
            using (myContext db1 = new myContext())
            {
                var orders = db1.AwaitOrders.ToList();
                return orders;
            }
        }

        public static void AddCollectionOrder(int orderId)
        {
            using (myContext db1 = new myContext())
            {
                var awaitOrder = new AwaitOrder() { OrderId = orderId };
                var result = db1.AwaitOrders.Add(awaitOrder);
                db1.SaveChanges();
            }
        }

        public static void RemoveCollectionOrder(int Id)
        {
            using (myContext db1 = new myContext())
            {
                var order = db1.AwaitOrders.First(m => m.Id == Id);
                //db1.AwaitOrders.Remove(order);
                db1.Entry(order).State = EntityState.Deleted;
                db1.SaveChanges();
            }
        }
        public static int GetCollectionCount()
        {
            using (myContext db1 = new myContext())
            {
                var count = db1.AwaitOrders.ToList().Count;
                return count;
            }
        }
        #endregion

        #region Delivery To Despatch

        public static List<DeliverytoDespatch> GetUnAssignedDeliveries()
        {
            using (myContext db1 = new myContext())
            {
                var deliveries = db1.DeliveriestoDespatch.Where(m => m.AssignedDriverId == 0).ToList();
                return deliveries;
            }
        }
        public static int GetUnAssignedDeliveriesCount()
        {
            using (myContext db1 = new myContext())
            {
                var count = db1.DeliveriestoDespatch.Where(m => m.AssignedDriverId == 0).ToList().Count;
                return count;
            }
        }
        public static int GetDriverPaymentsCount()
        {
            var r = (from d in db.DeliveriestoDespatch
                     where d.DriverPaymentStatus == false && d.AssignedDriverId != 0
                     group d by d.AssignedDriverId into driver
                     select new
                     {
                         count = driver.Count()
                     }).ToList();
            return r.Count;
            //return db.DeliveriestoDespatch.GroupBy(d=>d.AssignedDriverId).Where(m => m.DriverPaymentStatus == false && m.AssignedDriverId != 0).Count();
        }
        public static void AddDeliverytoDespatch(int orderId)
        {
            using (myContext db1 = new myContext())
            {
                var deliveryOrder = new DeliverytoDespatch()
                {
                    OrderId = orderId,
                    AssignedDriverId = 0
                };
                var result = db1.DeliveriestoDespatch.Add(deliveryOrder);
                db1.SaveChanges();
            }
        }
        public static void AssignDriver(int DeliveryOrderId, int DriverId)
        {
            var delivery = db.DeliveriestoDespatch.Find(DeliveryOrderId);
            delivery.AssignedDriverId = DriverId;
            db.Entry(delivery).State = EntityState.Modified;
            db.SaveChanges();
        }
        public static List<DeliverytoDespatch> GetUnPaidDeliveries()
        {
            return db.DeliveriestoDespatch.Where(m => m.DriverPaymentStatus == false && m.AssignedDriverId != 0).ToList();
        }

        public static List<DeliverytoDespatch> GetDriverDeliveries(int id)
        {
            return db.DeliveriestoDespatch.Where(d => d.AssignedDriverId == id && d.DriverPaymentStatus == false).ToList();
        }
        #endregion

        #region Statff
        public static List<Staff> GetAllDriversFromStatff()
        {
            using (myContext db1 = new myContext())
            {
                var drivers = db1.Staff.Where(m => m.Driver == true).ToList();
                return drivers;
            }
        }
        public static string GetDriverName(int id)
        {
            return db.Staff.Single(m => m.Id == id && m.Driver == true).Name;
        }
        #endregion

        #region Reporting

        public static List<Order> GetOrders(DateTime from, DateTime to)
        {
            var records = db.Orders.Where(o => o.Date >= from && o.Date <= to).ToList();
            return records;
        }

        //public static decimal WeeklySale()
        //{
        //    DateTime date = DateTime.Now.Date;
        //    var amount = db.Orders.Where(o => o.Date == date).Select(od => od.TotalAmount - od.Discount).Sum();
        //    return amount;
        //}

        //public static decimal MonthlySale(DateTime date)
        //{
        //    var amount = (from o in db.Orders
        //                  where o.Date.Month == date.Month && o.Date.Year == date.Year
        //                  select (o.TotalAmount - o.Discount)
        //                    ).Sum();
        //    return amount;
        //}

        #endregion
    }

    public class ReportModel
    {
        public DateTime date { get; set; }
        public decimal SalesAmount { get; set; }
    }
}
