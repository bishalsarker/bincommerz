using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Orders
{
    public class OrderPaymentLogResponse
    {
        public string OrderId { get; set; }

        public string TransactionMethod { get; set; }

        public double Amount { get; set; }

        public string PaymentNotes { get; set; }

        public string TransactionType { get; set; }

        public DateTime LogDateTime { get; set; }
    }
}
