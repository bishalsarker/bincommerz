using AutoMapper;
using BComm.PM.Dto.Payloads;
using BComm.PM.Dto.Products;
using BComm.PM.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<ProductPayload, Product>();

            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId))
                .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.ImageDirectory + src.ImageUrl));
        }
    }
}
