using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Repositories.Common;
using BComm.PM.Models.Coupons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BComm.PM.Repositories.Queries;
using BComm.PM.Dto.Coupons;

namespace BComm.PM.Services.Coupons
{
    public class CouponService : ICouponService
    {
        private readonly ICommandsRepository<Coupon> _couponCommandsRepository;
        private readonly ICouponQueryRepository _couponQueryRepository;
        private readonly IMapper _mapper;

        public CouponService(
            ICommandsRepository<Coupon> couponCommandsRepository,
            ICouponQueryRepository couponQueryRepository,
            IMapper mapper)
        {
            _couponCommandsRepository = couponCommandsRepository;
            _couponQueryRepository = couponQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> AddNewCoupon(CouponPayload newCouponRequest, string shopId)
        {
            try
            {
                var existingCouponModel = await _couponQueryRepository.GetCouponByCode(newCouponRequest.Code, shopId);

                if (existingCouponModel != null)
                {
                    throw new Exception("Coupon code exists already");
                }

                var couponModel = _mapper.Map<Coupon>(newCouponRequest);
                couponModel.HashId = Guid.NewGuid().ToString("N");
                couponModel.CreatedOn = DateTime.UtcNow;
                couponModel.ShopId = shopId;
                await _couponCommandsRepository.Add(couponModel);

                return new Response()
                {
                    Data = couponModel.HashId,
                    Message = "Coupon Created Successfully",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Message = "Error: " + ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> UpdateCoupon(CouponUpdatePayload newCouponRequest)
        {
            try
            {
                var existingCouponModel = await _couponQueryRepository.GetCouponById(newCouponRequest.Id);

                if (existingCouponModel == null)
                {
                    throw new Exception("Coupon doesn't exist");
                }

                existingCouponModel.Code = newCouponRequest.Payload.Code;
                existingCouponModel.Discount = newCouponRequest.Payload.Discount;
                existingCouponModel.IsActive = newCouponRequest.Payload.IsActive;

                await _couponCommandsRepository.Update(existingCouponModel);

                return new Response()
                {
                    Data = existingCouponModel.HashId,
                    Message = "Coupon Updated Successfully",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Message = "Error: " + ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> DeleteCoupon(string couponId)
        {
            try
            {
                var existingCouponModel = await _couponQueryRepository.GetCouponById(couponId);

                if (existingCouponModel == null)
                {
                    throw new Exception("Coupon doesn't exist");
                }

                await _couponCommandsRepository.Delete(existingCouponModel);

                return new Response()
                {
                    Data = existingCouponModel.HashId,
                    Message = "Coupon Deleted Successfully",
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Message = "Error: " + ex.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetAllCoupons(string shopId)
        {
            var couponModels = await _couponQueryRepository.GetAllCoupons(shopId);

            return new Response()
            {
                Data = _mapper.Map<IEnumerable<CouponResponse>>(couponModels),
                IsSuccess = true
            };
        }

        public async Task<Response> GetCouponById(string couponId)
        {
            var couponModel = await _couponQueryRepository.GetCouponById(couponId);

            return new Response()
            {
                Data = _mapper.Map<CouponResponse>(couponModel),
                IsSuccess = true
            };
        }
    }
}
