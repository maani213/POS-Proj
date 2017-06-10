using DataLayer.DAC;
using ProjectMy.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        [HttpGet]
        public ActionResult DailySales()
        {
            return View();
        }

        [HttpGet]
        public ActionResult WeeklySales()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MonthlySales()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DailySales(DateTime From, DateTime To)
        {
            var result = DAC.GetOrders(From, To);
            var amount = from o in result
                         group o by o.Date into grp
                         select new DailySalesReportViewModel() { Date = grp.Key.Date.ToString("dd/MM/yyyy"), TotalSale = grp.Sum(g => g.TotalAmount) / 2, Day = grp.Key.ToString("dddd") };

            return PartialView("_DailySaleReport", amount);
        }

        [HttpPost]
        public ActionResult WeeklySales(DateTime From, DateTime To)
        {
            var result = DAC.GetOrders(From, To);
            var weekGroups = result.Select(p => new
            {
                Project = p,
                Year = p.Date.Year,
                Week = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear
                                            (p.Date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)
            })
                            .GroupBy(x => new { x.Year, x.Week })
                            .Select((g, i) => new
                            {
                                WeekGroup = g,
                                WeekNum = i + 1,
                                Year = g.Key.Year,
                                CalendarWeek = g.Key.Week
                            });

            //foreach (var projGroup in weekGroups)
            //{
            //    Console.WriteLine("Week " + projGroup.WeekNum);
            //    foreach (var proj in projGroup.WeekGroup)
            //        Console.WriteLine("{0} {1} {2}",
            //            proj.Project.Name,
            //            proj.Project.DateStart.ToString("d"),
            //            proj.Project.ID);
            //}
            return Json(weekGroups, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MonthlySales(DateTime From, DateTime To)
        {
            var result = DAC.GetOrders(From, To);
            var amount = from o in result
                         group o by o.Date.ToString("MMMM") into grp
                         select new MonthlySalesViewModel() { MonthName = grp.Key, TotalSale = grp.Sum(g => g.TotalAmount) / 2 };

            return PartialView("_MonthlySaleReport", amount);
        }
    }
}