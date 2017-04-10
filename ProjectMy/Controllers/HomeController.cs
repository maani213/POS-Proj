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

    }
}