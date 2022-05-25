using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.UrlMappings
{
    public class HerokuDomainRequest
    {
        public string hostname { get; set; }

        public object sni_endpoint { get; set; }
    }
}
