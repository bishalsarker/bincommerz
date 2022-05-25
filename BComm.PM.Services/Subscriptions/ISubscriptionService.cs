using BComm.PM.Dto.Common;
using BComm.PM.Models.Subscriptions;
using System.Threading.Tasks;

namespace BComm.PM.Services.Subscriptions
{
    public interface ISubscriptionService
    {
        Task<Subscription> GetSubscription(string shopId);
        Task<Response> GetSubscriptionStatus(string shopId);
    }
}