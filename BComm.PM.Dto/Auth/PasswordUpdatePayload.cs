using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Auth
{
    public class PasswordUpdatePayload
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
