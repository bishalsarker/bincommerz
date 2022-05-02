using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.UrlMappings
{
    public class DomainMappingResponse
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string DnsTarget { get; set; }
    }
}
