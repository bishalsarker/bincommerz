using AutoMapper;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Orders;
using BComm.PM.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    class OrderMappings : Profile
    {
        public OrderMappings()
        {
            CreateMap<OrderPayload, Order>();
            CreateMap<Product, OrderItemModel>()
                .ForMember(dest => dest.ProductId,
                opt => opt.MapFrom(src => src.HashId));
        }
    }
}
