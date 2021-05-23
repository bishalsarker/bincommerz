using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Orders
{
    public class OrderItemResponse
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double Quantity { get; set; }
    }
}
