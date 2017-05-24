using DataLayer.DAC;
using DataLayer.Entities;
using DataLayer.Models;
using ProjectMy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMy.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        DAC data = new DAC();
        public ActionResult MainMenu()
        {
            return View();
        }

        public ActionResult Login()
        {
            List<String> names = new List<string>()
            {
                "Admin",
                "Houshang",
                "David",
                "Paula",
                "Lean",
                "Ezima"
            };
            return View(names);
        }

        [HttpGet]
        public ActionResult TakeAway(int categoryId = 1)
        {
            if (CustomerViewModel.OrderType == null)
            {
                CustomerViewModel.OrderType = "Take Away";
            }
            ViewData["CategoryID"] = categoryId;
            var orderdetails = (List<OrderDetail>)TempData["orderlist"];
            RepeatOrderViewModel list = new RepeatOrderViewModel()
            {
                OrderDetails = new List<OrderDetail>(),
                Total = 0
            };
            if (orderdetails != null)
            {
                RepeatOrderViewModel order = new RepeatOrderViewModel();
                order.OrderDetails = orderdetails;
                order.Total = orderdetails.Sum(m => m.Amount);
                ViewData["OrderDetailsList"] = order;
            }
            else
            {
                ViewData["OrderDetailsList"] = list;
            }

            return View();
        }


        public PartialViewResult ItemsView(string deptName = "Pizza")
        {
            return PartialView("_ItemsView");
        }

        [HttpPost]
        public JsonResult CompleteOrder(List<OrderDetail> orders)
        {
            TempData["ordersList"] = orders;
            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult OrderComplete(List<OrderDetail> orders)
        {
            if (CustomerViewModel.customer != null)
            {
                ViewData["CustomerName"] = CustomerViewModel.customer.FirstName;
                ViewData["CustomerAddress"] = CustomerViewModel.customer.Address1;
            }
            return View(orders);
        }

        [HttpPost]
        public JsonResult PrintOrder(PrintModel model)
        {
            model.PrintReceiptForTransaction();
            return Json("Order Printed", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ClearCash(List<OrderDetail> ordersDetails, Order order)
        {
            order.OrderTypeName = CustomerViewModel.OrderType;
            int NewCustomerId = 0;
            order.Date = DateTime.Now.Date;
            order.Status = "Paid";
            if (CustomerViewModel.customer != null)
            {
                NewCustomerId = DAC.AddOrUpdateCustomer(CustomerViewModel.customer);
                var NewOrder = DAC.AddOrder(order);
                NewOrder.CustomerId = NewCustomerId;

                if (CustomerViewModel.OrderType.ToLower().Contains("collection"))
                {
                    DAC.AddCollectionOrder(NewOrder.OrderId);
                }

                foreach (var item in ordersDetails)
                {
                    item.OrderId = NewOrder.OrderId;
                    DAC.AddOrderDetails(item);
                }
                CustomerViewModel.customer = null;
                CustomerViewModel.OrderType = null;
                return Json("Order Completed", JsonRequestBehavior.AllowGet);
            }

            else
            {
                var NewOrder1 = DAC.AddOrder(order);
                foreach (var item in ordersDetails)
                {
                    item.OrderId = NewOrder1.OrderId;
                    DAC.AddOrderDetails(item);
                }
                CustomerViewModel.customer = null;
                CustomerViewModel.OrderType = null;

                return Json("Order Completed", JsonRequestBehavior.AllowGet);
            }

        }

        [ChildActionOnly]
        public PartialViewResult GetCategories()
        {
            return PartialView("_Categories", DAC.GetAllCategories());
        }

        [HttpGet]
        public PartialViewResult TakeAwayItems(int categoryId = 1)
        {
            List<Item> Items = DAC.GetItemsByCategoryId(categoryId);
            List<ItemViewModel> ViewItems = new List<ItemViewModel>();
            foreach (var item in Items)
            {
                ItemViewModel viewitem = new ItemViewModel();
                viewitem.BackgroundColor = item.BackgroundColor;
                viewitem.TextColor = item.TextColor;
                viewitem.TextStyle = item.TextStyle;
                viewitem.Id = item.Id;
                viewitem.Title = item.Title;
                viewitem.CategoryId = item.CategoryId;
                viewitem.FontSize = item.FontSize;
                viewitem.Toppings = item.Toppings;
                if (item.IsBold)
                {
                    viewitem.fontWeight = "Bold";
                }
                else
                {
                    viewitem.fontWeight = "normal";
                }
                if (item.IsItalic)
                {
                    viewitem.fontStyle = "italic";
                }
                else
                {
                    viewitem.fontStyle = "normal";
                }

                ViewItems.Add(viewitem);
            }

            return PartialView("_TakeAwayItems", ViewItems);

            //List<Item> model = new List<Item>();
            //model = DAC.GetItemsByCategoryId(categoryId);

            //return PartialView("_TakeAwayItems", model);
        }
        [HttpGet]
        public JsonResult GetItemPrice(int itemID, int sizeId)
        {
            decimal price = DAC.GetItemPrice(itemID, sizeId);
            return Json(price, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetExtrasPrices(List<int> toppingIds, int sizeId = 0)
        {
            decimal price = DAC.GetExtrasPrices(toppingIds, sizeId);
            return Json(price, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult GetSizes(int categoryId)
        {
            return PartialView("_GetSizes", DAC.GetSizesByCategoryId(categoryId));
        }

        [HttpGet]
        public PartialViewResult GetExtras(int categoryId)
        {
            return PartialView("_GetExtras", DAC.GetExtrasByCategoryId(categoryId));
        }

        [ChildActionOnly]
        public PartialViewResult GetSizesPartial(int categoryId)
        {
            return PartialView("_GetSizes", DAC.GetSizesByCategoryId(categoryId));
        }

        [ChildActionOnly]
        public PartialViewResult GetExtrasPartial(int categoryId)
        {
            return PartialView("_GetExtras", DAC.GetExtrasByCategoryId(categoryId));
        }

        [HttpGet]
        public ActionResult TakeAwayHome()
        {
            var result = DAC.GetAllCategories();
            if (result != null)
            {
                return View(result);
            }
            else
            {
                return View(new List<Category>());
            }
        }

        [HttpGet]
        public ActionResult Discounts()
        {
            var list = new List<Discount>();
            list = DAC.GetAllDiscounts();
            return PartialView("_Discounts", list);
        }

    }
}