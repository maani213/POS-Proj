using DataLayer.DAC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMy.Controllers
{
    public class ReportsManagementController : Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult SalesReport()
        {
            return View();
        }

        public ActionResult TodaysSale()
        {
            var result = DAC.TodaysSale();
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}