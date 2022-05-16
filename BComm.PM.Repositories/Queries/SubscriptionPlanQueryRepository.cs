using BComm.PM.Models.Subscriptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public class SubscriptionPlanQueryRepository
    {
        private readonly IEnumerable<Plan> _plans = new List<Plan>()
        {
            new Plan()
            {
                HashId = "fe259c4892564ea5b14710d332dae053",
                PlanName = "Free",
                CanAddCustomDomain = false,
                ProductEntryLimit = 30,
                DurationType = DurationTypes.Monthly
            },
            new Plan()
            {
                HashId = "0856087606974b53ba0711b20b90d629",
                PlanName = "Basic",
                CanAddCustomDomain = true,
                ProductEntryLimit = 500,
                DurationType = DurationTypes.Monthly
            },
            new Plan() {
                HashId = "878dfe2e45354dd5b60c38d34fce3177",
                PlanName = "Enterprise",
                CanAddCustomDomain = true,
                ProductEntryLimit = 0,
                DurationType = DurationTypes.Monthly
            }
        };

        public async Task<Plan> GetPlan(string planId)
        {
            return await Task.Run(() => _plans.Where(x => x.HashId == planId).FirstOrDefault());
        }
    }
}
