using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Dto
{
    public class SubscriptionResponse
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public string PlanName { get; set; }

        public int ProductEntryLimit { get; set; }

        public int TotalProducts { get; set; }

        public bool CanAddCustomDomain { get; set; }

        public DateTime ValidTill { get; set; }

        public DateTime NextPaymentOn { get; set; }
    }
}
