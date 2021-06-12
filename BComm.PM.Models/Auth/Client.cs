using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Models.Auth
{
    public class Client
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string AuthCallback { get; set; }
    }
}
