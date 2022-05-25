using BComm.PM.Models.Subscriptions;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ISubscriptionQueryRepository
    {
        Task<Subscription> GetSubscription(string userId);
    }
}