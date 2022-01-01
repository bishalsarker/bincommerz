using AutoMapper;
using BComm.PM.Dto.Images;
using BComm.PM.Dto.Payloads;
using BComm.PM.Dto.Products;
using BComm.PM.Models.Images;
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

            CreateMap<ProductUpdatePayload, Product>();

            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId))
                .ForMember(dest => dest.InStock,
                opt => opt.MapFrom(src => src.StockQuantity > 0))
                .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src => src.ImageDirectory + src.ImageUrl))
                .ForMember(dest => dest.Discount,
                opt => opt.MapFrom(src => Math.Round(src.Discount, 2)));


            CreateMap<Image, ImageResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId))
                .ForMember(dest => dest.OriginalImage,
                opt => opt.MapFrom(src => src.Directory + src.OriginalImage))
                .ForMember(dest => dest.ThumbnailImage,
                opt => opt.MapFrom(src => src.Directory + src.ThumbnailImage));
        }
    }
}
