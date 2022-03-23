using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto.Payloads
{
    public class DeliveryChargeUpdatePayload
    {
        public string Id { get; set; }

        public DeliveryChargePayload Payload { get; set; }
    }
}
