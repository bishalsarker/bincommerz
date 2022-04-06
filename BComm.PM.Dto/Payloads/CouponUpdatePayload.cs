using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class CouponUpdatePayload
    {
        public string Id { get; set; }

        public CouponPayload Payload { get; set; }
    }
}
