using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Common
{
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; }
    }
}
