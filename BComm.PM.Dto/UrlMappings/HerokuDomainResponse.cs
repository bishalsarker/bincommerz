using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.UrlMappings
{
    public class HerokuDomainResponse
    {
        public object acm_status { get; set; }

        public object acm_status_reason { get; set; }

        public App app { get; set; }

        public string cname { get; set; }

        public DateTime created_at { get; set; }

        public string hostname { get; set; }

        public string id { get; set; }

        public string kind { get; set; }

        public string status { get; set; }

        public DateTime updated_at { get; set; }

        public object sni_endpoint { get; set; }
    }

    public class App
    {
        public string id { get; set; }

        public string name { get; set; }
    }
}
