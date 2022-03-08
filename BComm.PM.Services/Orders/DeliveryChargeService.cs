using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Orders;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Orders;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Orders
{
    public class DeliveryChargeService : IDeliveryChargeService
    {
        private readonly ICommandsRepository<DeliveryCharge> _deliveryChargeCommandsRepository;
        private readonly IShopQueryRepository _shopQueryRepository;
        private readonly IDeliveryChargeQueryRepository _deliveryChargeQueryRepository;
        private readonly IMapper _mapper;

        public DeliveryChargeService(
            ICommandsRepository<DeliveryCharge> deliveryChargeCommandsRepository,
            IDeliveryChargeQueryRepository deliveryChargeQueryRepository,
            IShopQueryRepository shopQueryRepository,
            IMapper mapper)
        {
            _deliveryChargeCommandsRepository = deliveryChargeCommandsRepository;
            _deliveryChargeQueryRepository = deliveryChargeQueryRepository;
            _shopQueryRepository = shopQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> AddNewDeliveryCharge(DeliveryChargePayload deliveryChargeRequest, string shopId)
        {
            try
            {
                var shopModel = await _shopQueryRepository.GetShopById(shopId);
                if (shopModel != null)
                {
                    var newDeliveryChargeModel = _mapper.Map<DeliveryCharge>(deliveryChargeRequest);
                    newDeliveryChargeModel.HashId = Guid.NewGuid().ToString("N");
                    newDeliveryChargeModel.ShopId = shopId;

                    await _deliveryChargeCommandsRepository.Add(newDeliveryChargeModel);

                    return new Response()
                    {
                        Data = new { id = newDeliveryChargeModel.HashId },
                        Message = "New Delivery Charge Added",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Couldn't find specific shop",
                        IsSuccess = false
                    };
                }

            }
            catch (Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> UpdateDeliveryCharge(DeliveryChargeUpdatePayload deliveryChargeUpdateRequest)
        {
            try
            {
                var existingDeliveryChargeModel = await _deliveryChargeQueryRepository.GetDeliveryChargeById(deliveryChargeUpdateRequest.Id);
                if (existingDeliveryChargeModel != null)
                {
                    var newDeliveryChargeModel = deliveryChargeUpdateRequest.Payload;
                    existingDeliveryChargeModel.Title = newDeliveryChargeModel.Title;
                    existingDeliveryChargeModel.Amount = newDeliveryChargeModel.Amount;

                    await _deliveryChargeCommandsRepository.Update(existingDeliveryChargeModel);

                    return new Response()
                    {
                        Data = new { id = existingDeliveryChargeModel.HashId },
                        Message = "Delivery Charge Updated",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Couldn't find specific delivery charge",
                        IsSuccess = false
                    };
                }

            }
            catch (Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> DeleteDeliveryCharge(string deliveryChargeId)
        {
            try
            {
                var existingDeliveryChargeModel = await _deliveryChargeQueryRepository.GetDeliveryChargeById(deliveryChargeId);
                if (existingDeliveryChargeModel != null)
                {
                    await _deliveryChargeCommandsRepository.Delete(existingDeliveryChargeModel);

                    return new Response()
                    {
                        Message = "Delivery Charge Deleted",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Couldn't find specific delivery charge",
                        IsSuccess = false
                    };
                }

            }
            catch (Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetAllDeliveryCharges(string shopId)
        {
            var deliveryChargeModels = await _deliveryChargeQueryRepository.GetAllDeliveryCharges(shopId);
            var deliveryChargeResponses = _mapper.Map<IEnumerable<DeliveryChargeResponse>>(deliveryChargeModels);

            return new Response()
            {
                Data = deliveryChargeResponses,
                IsSuccess = true
            };
        }

        public async Task<Response> GetDeliveryCharge(string deliveryChargeId)
        {
            var deliveryChargeModels = await _deliveryChargeQueryRepository.GetDeliveryChargeById(deliveryChargeId);
            var deliveryChargeResponses = _mapper.Map<DeliveryChargeResponse>(deliveryChargeModels);

            return new Response()
            {
                Data = deliveryChargeResponses,
                IsSuccess = true
            };
        }
    }
}
