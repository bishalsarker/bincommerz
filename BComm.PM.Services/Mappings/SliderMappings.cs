using AutoMapper;
using BComm.PM.Dto.Widgets;
using BComm.PM.Models.Widgets;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class SliderMappings : Profile
    {
        public SliderMappings()
        {
            CreateMap<SliderImagePayload, SliderImage>();

            //CreateMap<ProductUpdatePayload, Product>();

            //CreateMap<Product, ProductResponse>()
            //    .ForMember(dest => dest.Id,
            //    opt => opt.MapFrom(src => src.HashId))
            //    .ForMember(dest => dest.InStock,
            //    opt => opt.MapFrom(src => src.StockQuantity > 0))
            //    .ForMember(dest => dest.ImageUrl,
            //    opt => opt.MapFrom(src => src.ImageDirectory + src.ImageUrl));

            //CreateMap<Image, ImageResponse>()
            //    .ForMember(dest => dest.Id,
            //    opt => opt.MapFrom(src => src.HashId))
            //    .ForMember(dest => dest.OriginalImage,
            //    opt => opt.MapFrom(src => src.Directory + src.OriginalImage))
            //    .ForMember(dest => dest.ThumbnailImage,
            //    opt => opt.MapFrom(src => src.Directory + src.ThumbnailImage));
        }
    }
}
