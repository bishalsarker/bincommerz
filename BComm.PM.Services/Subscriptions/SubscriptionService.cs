using AutoMapper;
using BComm.PM.Dto;
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
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(
            ISubscriptionQueryRepository subscriptionQueryRepository,
            IProductQueryRepository productQueryRepository,
            IShopQueryRepository shopQueryRepository,
            IMapper mapper)
        {
            _subscriptionQueryRepository = subscriptionQueryRepository;
            _shopQueryRepository = shopQueryRepository;
            _productQueryRepository = productQueryRepository;
            _mapper = mapper;
        }

        public async Task<Subscription> GetSubscription(string shopId)
        {
            var shop = await _shopQueryRepository.GetShopById(shopId);
            var subscription = await _subscriptionQueryRepository.GetSubscription(shop.UserHashId);

            return subscription;
        }

        public async Task<Response> GetSubscriptionStatus(string shopId)
        {
            try
            {
                var shop = await _shopQueryRepository.GetShopById(shopId);
                var subscription = await _subscriptionQueryRepository.GetSubscription(shop.UserHashId);

                if (subscription == null)
                {
                    throw new Exception("Invalid subscription");
                }

                var subResponse = _mapper.Map<SubscriptionResponse>(subscription);
                subResponse.TotalProducts = await _productQueryRepository.GetProductCount(shopId);

                if (!subscription.IsActive)
                {
                    subResponse.CanAddCustomDomain = false;
                }

                return new Response()
                {
                    Data = subResponse,
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Message = "Category Orders Couldn't Be Updated",
                    IsSuccess = false
                };
            }
        }

    }
}
