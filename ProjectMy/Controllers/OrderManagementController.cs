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
                    AwaitOrderId = ord.Id,
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
            var AllOrders = DAC.GetAllOrdersDesc();
            List<OrderViewModel> OrdersList = new List<OrderViewModel>();
            foreach (var order in AllOrders)
            {
                var customer = DAC.FindCustomerById(order.CustomerId);
                OrderViewModel listItem = new OrderViewModel()
                {
                    OrderId = order.OrderId,
                    OrderNo = order.OrderId,
                    Date = order.Date.ToString("dd/MM/yyyy"),
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                };
                if (customer != null)
                {
                    listItem.Name = customer.FirstName + " " + customer.SurName;
                    listItem.Telephone = customer.Phone;
                }
                OrdersList.Add(listItem);
            }
            return View(OrdersList);
        }
        [HttpPost]
        public ActionResult RemoveAwaitOrder(int id)
        {
            DAC.RemoveCollectionOrder(id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}