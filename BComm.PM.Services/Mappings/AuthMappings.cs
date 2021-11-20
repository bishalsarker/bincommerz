using AutoMapper;
using BComm.PM.Dto.Auth;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class AuthMappings : Profile
    {
        public AuthMappings()
        {
            CreateMap<UserAccountPayload, User>();

            CreateMap<Shop, ShopResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId));

            CreateMap<ShopUpdatePayload, Shop>();
        }
    }
}
