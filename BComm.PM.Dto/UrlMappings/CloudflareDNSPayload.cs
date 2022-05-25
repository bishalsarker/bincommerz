using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.UrlMappings
{
    public class CloudflareDNSPayload
    {
        public string type { get; set; }

        public string name { get; set; }

        public string content { get; set; }

        public int ttl { get; set; }

        public int priority { get; set; }

        public bool proxied { get; set; }
    }
}
