using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Models.Subscriptions
{
    public class Plan : WithHashId
    {
        public string PlanName { get; set; }

        public int ProductEntryLimit { get; set; }

        public bool CanAddCustomDomain { get; set; }

        public DurationTypes DurationType { get; set; }

        public SubscriptionTypes SubscriptionType { get; set; }
    }
}
