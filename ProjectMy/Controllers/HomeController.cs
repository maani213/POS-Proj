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
            ViewBag.CategoryID = categoryId;
            return View();
        }


        public PartialViewResult ItemsView(string deptName = "Pizza")
        {
            return PartialView("_ItemsView");
        }

        [HttpPost]
        public JsonResult CompleteOrder(List<OrderDetails> orders)
        {
            TempData["ordersList"] = orders;
            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        public ActionResult OrderComplete(List<OrderDetails> orders)
        {
            List<OrderDetails> orders1 = TempData["ordersList"] as List<OrderDetails>;
            return View(orders1);
        }

        [HttpPost]
        public JsonResult ClearCash(List<OrderViewModel> orders, string CustomerName = "Test", int totalpaid = 0, int balance = 0)
        {
            AddOrderModel orderModel = new AddOrderModel();
            orderModel.order.CustomerName = CustomerName;
            //orderModel.orderDetail.ItemName = orders.ItemName;
            return Json("Order Completed", JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult GetCategories()
        {
            return PartialView("_Categories", DAC.GetAllCategories());
        }

        [HttpGet]
        public PartialViewResult TakeAwayItems(int categoryId = 1)
        {
            List<Item> model = new List<Item>();
            model = DAC.GetItemsByCategoryId(categoryId);

            return PartialView("_TakeAwayItems", model);
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
                return View(new List<Categories>());
            }
        }
    }
}