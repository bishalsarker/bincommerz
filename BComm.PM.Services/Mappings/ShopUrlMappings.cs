using AutoMapper;
using BComm.PM.Dto.UrlMappings;
using BComm.PM.Models.UrlMappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class ShopUrlMappings : Profile
    {
        public ShopUrlMappings()
        {
            CreateMap<UrlMappings, UrlMappingResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId));
        }
    }
}
