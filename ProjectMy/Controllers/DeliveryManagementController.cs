using DataLayer.DAC;
using ProjectMy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectMy.Controllers
{
    public class DeliveryManagementController : Controller
    {
        [HttpGet]
        public ActionResult AssigntoDriver()
        {
            var Deliveries = DAC.GetUnAssignedDeliveries();
            List<DeliveriesViewModel> deliveryorders = new List<DeliveriesViewModel>();
            foreach (var ord in Deliveries)
            {
                var order = DAC.GetOrder(ord.OrderId);
                var customer = DAC.FindCustomerById(order.CustomerId);
                DeliveriesViewModel deliveryOrder = new DeliveriesViewModel()
                {
                    DeliveryId = ord.Id,
                    OrderNo = order.OrderId,
                    Time = order.Date.ToString("HH:mm:ss"),
                    Address = customer.Address1 + " " + customer.Address2,
                    TotalAmount = order.TotalAmount,
                    Name = customer.FirstName + " " + customer.SurName,
                    Telephone = customer.Phone,
                    PostCode = customer.PostCode
                };

                deliveryorders.Add(deliveryOrder);
            }
            return View(deliveryorders);
        }

        [HttpGet]
        public ActionResult SelectDriver()
        {
            var Drivers = DAC.GetAllDriversFromStatff();
            return PartialView("_SelectDriver", Drivers);
        }

        [HttpPost]
        public ActionResult AddtoDriverPayment(int[] DeliveryOrders, int DriverId)
        {
            foreach (var id in DeliveryOrders)
            {
                DAC.AssignDriver(id, DriverId);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DriverPayments()
        {
            var result = DAC.GetUnPaidDeliveries();
            List<DriverPaymentViewModel> driverpaymentLsit = new List<DriverPaymentViewModel>();

            foreach (var item in result)
            {
                DriverPaymentViewModel model = new DriverPaymentViewModel()
                {
                    DriverId = item.AssignedDriverId,
                    Name = DAC.GetDriverName(item.AssignedDriverId),
                    Total = DAC.GetOrderTotal(item.OrderId),
                    Paid = 0,
                    Trips = 1
                };

                driverpaymentLsit.Add(model);
            }
            var answer = driverpaymentLsit.GroupBy(m => m.DriverId)
                            .Select(c => new DriverPaymentViewModel
                            {
                                Total = c.Sum(cc => cc.Total),
                                Trips = c.Count(),
                                DriverId = c.First().DriverId,
                                Name = c.First().Name,
                                Balance = c.Sum(cc => cc.Total) - c.First().Paid
                            }).ToList();

            return View(answer);
        }

        [HttpGet]
        public ActionResult ViewDriverPymentDetails(int id)
        {
            var Deliveries = DAC.GetDriverDeliveries(id);
            List<DeliveriesViewModel> deliveryorders = new List<DeliveriesViewModel>();
            foreach (var ord in Deliveries)
            {
                var order = DAC.GetOrder(ord.OrderId);
                var customer = DAC.FindCustomerById(order.CustomerId);
                DeliveriesViewModel deliveryOrder = new DeliveriesViewModel()
                {
                    DeliveryId = ord.Id,
                    OrderNo = order.OrderId,
                    Time = order.Date.ToString("dd/MM/yyyy") + " " + order.Date.ToString("HH:mm:ss"),
                    Address = customer.Address1 + " " + customer.Address2,
                    TotalAmount = order.TotalAmount,
                    Name = customer.FirstName + " " + customer.SurName,
                    Telephone = customer.Phone,
                    PostCode = customer.PostCode,
                    Status = ((Constants.PaymentStatus)order.Status).ToString()
                };

                deliveryorders.Add(deliveryOrder);
            }
            return View(deliveryorders);
        }

        [HttpGet]
        public ActionResult DriverPaymentView()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult MakePayment()
        {
            return PartialView("_MakePayment");
        }
    }
}