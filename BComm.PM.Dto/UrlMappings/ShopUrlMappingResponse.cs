using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.UrlMappings
{
    public class ShopUrlMappingResponse
    {
        public string AppUrl { get; set; }

        public IEnumerable<DomainMappingResponse> Domains { get; set; }

        public string DomainDNSValue { get; set; }
    }
}
