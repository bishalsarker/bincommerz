using BComm.PM.Models.Subscriptions;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ISubscriptionPlanQueryRepository
    {
        Task<Plan> GetPlan(string planId);
    }
}