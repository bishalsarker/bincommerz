using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Auth
{
    public class UserAccountPayload
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
