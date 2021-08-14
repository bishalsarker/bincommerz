using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Models.Auth
{
    public class ShopConfig
    {
        public string ShopId { get; set; }

        public string OrderCode { get; set; }

        public double ReorderLevel { get; set; }
    }
}
