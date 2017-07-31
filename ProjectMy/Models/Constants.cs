using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectMy.Models
{
    public class Constants
    {
        public enum OrderType
        {
            TakeAway = 1,
            Delivery = 2,
            Collection = 3
        }

        public enum PaymentStatus
        {
            Unpaid = 0,
            Paid = 1
        }

        public enum PaymentType
        {
            Cash = 1,
            CreditCard = 2,
            Check = 3,
            Voucher = 4
        }
    }
}