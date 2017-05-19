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
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult GetCustomer()
        {
            return PartialView("_GetCustomer", new Customer());
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
                if (CustomerViewModel.customer != null)
                {
                    ViewData["CustomerName"] = CustomerViewModel.customer.Name;
                    ViewData["CustomerAddress"] = CustomerViewModel.customer.Address;
                }

                var orders = DAC.GetCustomerOrders(CustomerViewModel.customer.Id);
                var OrderDetails = DAC.GetOrderDetails(orders[0].OrderId);
                return View("~/Views/Home/OrderComplete.cshtml", OrderDetails);
                //return RedirectToAction("OrderComplete","Home", OrderDetails);
            }
            else {
                return View("Index");
            }

        }
    }
}