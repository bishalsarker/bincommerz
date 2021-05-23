using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Orders
{
    public class OrderProcessLogResponse
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime LogDateTime { get; set; }
    }
}
