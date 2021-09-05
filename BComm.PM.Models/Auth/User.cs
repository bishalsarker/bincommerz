using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Models.Auth
{
    public class User
    {
        public string HashId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
