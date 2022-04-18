using AutoMapper;
using BComm.PM.Dto.Coupons;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Coupons;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class CouponMappings : Profile
    {
        public CouponMappings()
        {
            CreateMap<CouponPayload, Coupon>()
                .ForMember(dest => dest.DiscountType,
                opt => opt.MapFrom(src => MapDiscountType(src.DiscountType)));

            CreateMap<Coupon, CouponResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId))
                .ForMember(dest => dest.DiscountType,
                opt => opt.MapFrom(src => MapDiscountType(src.DiscountType)));
        }

        private CouponDiscountTypes MapDiscountType(string discountTypeInString)
        {
            CouponDiscountTypes discountType = CouponDiscountTypes.Percentage;

            switch(discountTypeInString)
            {
                case "percentage":
                    discountType = CouponDiscountTypes.Percentage;
                    break;
                case "amount":
                    discountType = CouponDiscountTypes.FixedAmount;
                    break;
            }

            return discountType;
        }

        private string MapDiscountType(CouponDiscountTypes discountTypeEmum)
        {
            string discountType = "percentage";

            switch (discountTypeEmum)
            {
                case CouponDiscountTypes.Percentage:
                    discountType = "percentage";
                    break;
                case CouponDiscountTypes.FixedAmount:
                    discountType = "amount";
                    break;
            }

            return discountType;
        }
    }
}
