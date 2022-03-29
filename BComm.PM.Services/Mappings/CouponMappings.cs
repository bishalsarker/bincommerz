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
            CreateMap<CouponPayload, Coupon>();

            CreateMap<Coupon, CouponResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId));
        }
    }
}
