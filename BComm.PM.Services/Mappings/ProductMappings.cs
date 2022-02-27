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
                .ForMember(dest => dest.DiscountInPercentage,
                opt => opt.MapFrom(src => GetDiscountInPercentage(src)));


            CreateMap<Image, ImageResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId))
                .ForMember(dest => dest.OriginalImage,
                opt => opt.MapFrom(src => src.Directory + src.OriginalImage))
                .ForMember(dest => dest.ThumbnailImage,
                opt => opt.MapFrom(src => src.Directory + src.ThumbnailImage));
        }

        private double GetDiscountInPercentage(Product product)
        {
            if (product.Discount > 0)
            {
                var productPriceAfterDiscount = product.Price - product.Discount;
                var discountInDouble = ((productPriceAfterDiscount / product.Price) * 100);

                if (discountInDouble > 0 && discountInDouble < 1)
                {
                    return 1;
                }
                else
                {
                    var discountPercentage = Math.Round(discountInDouble, MidpointRounding.AwayFromZero);
                    return discountPercentage;
                }
            }

            return 0;
        }
    }
}
