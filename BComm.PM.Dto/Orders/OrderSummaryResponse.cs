using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Orders
{
    public class OrderSummaryResponse
    {
        public int TotalOrder { get; set; }

        public int TotalIncompleteOrder { get; set; }

        public int TotalCompletedOrder { get; set; }

        public int TotalCanceledOrder { get; set; }
    }
}
