using DataLayer.DAC;
using ProjectMy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMy.Controllers
{
    public class OrderManagementController : Controller
    {
        [HttpGet]
        public ActionResult AwaitCollections()
        {
            var CollectionOrders = DAC.GetCollectionOrders();
            List<AwaitOrderModel> awaitorders = new List<AwaitOrderModel>();
            foreach (var ord in CollectionOrders)
            {
                var order = DAC.GetOrder(ord.OrderId);
                var customer = DAC.FindCustomerById(order.CustomerId);
                AwaitOrderModel collectionOrder = new AwaitOrderModel()
                {
                    AwaitOrderId = order.OrderId,
                    OrderNo = order.OrderId,
                    Date = order.Date.ToString("dd/MM/yyyy"),
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                    Collect = order.Date.ToString("HH:mm:ss"),
                    Name = customer.FirstName + " " + customer.SurName,
                    Telephone = customer.Phone
                };

                awaitorders.Add(collectionOrder);
            }
            return View(awaitorders);
        }

        [HttpGet]
        public ActionResult EditRecords()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RemoveAwaitOrder(int id)
        {
            DAC.RemoveCollectionOrder(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}