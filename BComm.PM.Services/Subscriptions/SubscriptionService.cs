using BComm.PM.Dto.Common;
using BComm.PM.Models.Subscriptions;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionQueryRepository _subscriptionQueryRepository;
        private readonly IShopQueryRepository _shopQueryRepository;

        public SubscriptionService(
            ISubscriptionQueryRepository subscriptionQueryRepository,
            IShopQueryRepository shopQueryRepository)
        {
            _subscriptionQueryRepository = subscriptionQueryRepository;
            _shopQueryRepository = shopQueryRepository;
        }

        public async Task<Subscription> GetSubscription(string shopId)
        {
            var shop = await _shopQueryRepository.GetShopById(shopId);
            var subscription = await _subscriptionQueryRepository.GetSubscription(shop.UserHashId);

            return subscription;
        }
    }
}
