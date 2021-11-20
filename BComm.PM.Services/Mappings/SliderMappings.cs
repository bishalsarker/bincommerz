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

            CreateMap<SliderPayload, Slider>()
                .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => ResolveSliderType(src.Type)));

            CreateMap<Slider, SliderResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId))
                .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => ResolveSliderType(src.Type)));

            //CreateMap<ProductUpdatePayload, Product>();

            CreateMap<SliderImage, SliderImageResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId))
                .ForMember(dest => dest.ImageURL,
                opt => opt.MapFrom(src => src.ImageDirectory + src.ImageName));
        }

        public static SliderTypes ResolveSliderType(string type)
        {
            switch (type)
            {
                case "image":
                    return SliderTypes.Image;
                case "card":
                    return SliderTypes.Card;
                default:
                    return SliderTypes.Image;
            }
        }

        public static string ResolveSliderType(SliderTypes type)
        {
            switch (type)
            {
                case SliderTypes.Image:
                    return "image";
                case SliderTypes.Card:
                    return "card";
                default:
                    return "image";
            }
        }
    }
}
