using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Models.Orders
{
    public class ProductOrderCount
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int OrderCount { get; set; }
    }
}
