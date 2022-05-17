using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto
{
    public class SubscriptionResponse
    {
        public bool IsActive { get; set; }

        public string PlanName { get; set; }

        public DateTime ValidTill { get; set; }

        public DateTime NextPaymentOn { get; set; }
    }
}
