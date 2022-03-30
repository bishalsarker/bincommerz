using AutoMapper;
using BComm.PM.Dto.Orders;
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
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId))
                .ForMember(dest => dest.Discount,
                opt => opt.MapFrom(src => src.CouponDiscount));

            CreateMap<DeliveryChargePayload, DeliveryCharge>();
            CreateMap<DeliveryCharge, DeliveryChargeResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId));

            CreateMap<Product, OrderItemModel>()
                .ForMember(dest => dest.ProductId,
                opt => opt.MapFrom(src => src.HashId));

            CreateMap<OrderItemModel, OrderItemResponse>();
            CreateMap<OrderProcessLog, OrderProcessLogResponse>();

            CreateMap<OrderPaymentPayload, OrderPaymentLog>();
            CreateMap<OrderPaymentLog, OrderPaymentLogResponse>();
        }
    }
}
