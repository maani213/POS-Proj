using DataLayer.DAC;
using DataLayer.Entities;
using ProjectMy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMy.Controllers
{
    public class DeliveryCollectionController : Controller
    {
        public ActionResult Index(string number = null)
        {
            ViewData["number"] = number;
            return View();
        }

        [HttpGet]
        public PartialViewResult GetCustomer(string number = null)
        {
            if (number != null)
            {
                var customer = DAC.FindCustomer("1", number);
                return PartialView("_GetCustomer", customer);
            }
            else
            {
                return PartialView("_GetCustomer", new Customer());
            }
        }

        [HttpGet]
        public PartialViewResult FindCustomer(string type, string value)
        {
            var customer = DAC.FindCustomer(type, value);
            return PartialView("_GetCustomer", customer);
        }

        [HttpPost]
        public ActionResult Delivery(Customer c)
        {
            if (ModelState.IsValid)
            {
                CustomerViewModel.customer = c;
                CustomerViewModel.OrderType = "Delivery";
                return RedirectToAction("TakeAway", "Home");
            }
            else
            {
                return PartialView("_GetCustomer");
            }
        }

        [HttpPost]
        public ActionResult Collection(Customer c)
        {
            if (ModelState.IsValid)
            {
                CustomerViewModel.customer = c;
                CustomerViewModel.OrderType = "Collection";
                return RedirectToAction("TakeAway", "Home");
            }
            else
            {
                return PartialView("_GetCustomer");
            }
        }

        [HttpPost]
        public ActionResult RepeatOrder(Customer c)
        {
            if (ModelState.IsValid)
            {
                CustomerViewModel.customer = c;
                List<OrderDetail> OrderDetails = new List<OrderDetail>();
                var orders = DAC.GetCustomerOrders(CustomerViewModel.customer.Id);
                if (orders !=null)
                {
                    OrderDetails = DAC.GetOrderDetails(orders.OrderId);
                }
                TempData["orderlist"] = OrderDetails;
                //return View("~/Views/Home/TakeAway.cshtml", OrderDetails);
                return RedirectToAction("TakeAway", "Home");
            }
            else
            {
                return View("Index");
            }

        }

        [HttpGet]
        public ActionResult AwaitCollections()
        {
            return View();
        }

    }
}