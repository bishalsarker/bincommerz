using BComm.PM.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BComm.PM.Models.Subscriptions
{
    [Table("subscriptions", Schema = "bcomm_user")]
    public class Subscription : WithHashId
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string PlanId { get; set; }

        public string PlanName { get; set; }

        public int ProductEntryLimit { get; set; }

        public bool CanAddCustomDomain { get; set; }

        public bool IsActive { get; set; }

        public SubscriptionStatus Status { get; set; }

        public DurationTypes DurationType { get; set; }

        public SubscriptionTypes SubscriptionType { get; set; }

        public int IntervalInMonths { get; set; }

        public DateTime ValidTill { get; set; }

        public DateTime NextPaymentOn { get; set; }
    }

    public enum SubscriptionTypes
    {
        Free = 0,
        Paid = 1
    }

    public enum DurationTypes
    {
        Monthly = 0,
        Yearly = 1
    }

    public enum SubscriptionStatus
    {
        Active = 0,
        Cancelled = 1,
        Halted = 2,
        Completed = 3,
        Pending = 4
    }
}
