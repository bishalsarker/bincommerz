using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Auth
{
    public class ShopResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Logo { get; set; }

        public string Url { get; set; }

        public string IPAddress { get; set; }

        public double ReorderLevel { get; set; }
    }
}
