using AutoMapper;
using BComm.PM.Dto;
using BComm.PM.Models.Subscriptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class SubscriptionMappings : Profile
    {
        public SubscriptionMappings()
        {
            CreateMap<Subscription, SubscriptionResponse>();
        }
    }
}
