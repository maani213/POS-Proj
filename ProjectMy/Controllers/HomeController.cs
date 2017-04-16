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

        public ActionResult TakeAway()
        {
            return View();
        }

       
        public JsonResult AddItem(Pizza model)
        {
            data.AddItem(model);
            return Json("item added", JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ItemsView(string deptName = "Pizza")
        {
            List<Pizza> dummy = new List<Pizza>() {
                new Pizza() { BackgroundColor="green" ,Title="Pizza" , Price="900" },
                new Pizza() { BackgroundColor="pink" ,Title="Burger" , Price="350" },
                new Pizza() { BackgroundColor="blue" ,Title="Cake" , Price="500" },
                new Pizza() { BackgroundColor="blue" ,Title="Cake" , Price="500" }

            };
            return PartialView("_ItemsView", dummy);
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

        public JsonResult ClearCash(List<OrderDetails> orders , int totalpaid , int balance)
        {
            return Json("Order Completed", JsonRequestBehavior.AllowGet);
        }

        
    }
}