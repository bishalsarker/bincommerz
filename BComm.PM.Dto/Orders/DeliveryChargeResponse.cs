using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Orders
{
    public class DeliveryChargeResponse
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public double Amount { get; set; }
    }
}
