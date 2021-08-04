using BComm.PM.Dto.Tags;
using BComm.PM.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Reports
{
    public class OrderCountResponse
    {
        public int TotalOrderCount { get; set; }

        public IEnumerable<ProductOrderCount> OrderCounts { get; set; }

    }
}
