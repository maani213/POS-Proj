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

        public ActionResult ManagementSection()
        {
            return View();
        }

        public ActionResult DataFiles()
        {
            return View();
        }

        public ActionResult MenuItem()
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
                new Pizza() { BackgroundColor="green" ,Title="Test" , Price="500" },
                new Pizza() { BackgroundColor="pink" ,Title="Test" , Price="500" },
                new Pizza() { BackgroundColor="blue" ,Title="Test" , Price="500" }

            };
            return PartialView("_ItemsView", dummy);
        }
    }
}