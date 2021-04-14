using AutoMapper;
using BComm.PM.Dto.Payloads;
using BComm.PM.Dto.Tags;
using BComm.PM.Models.Tags;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class TagMappings : Profile
    {
        public TagMappings()
        {
            CreateMap<TagPayload, Tag>();
            CreateMap<Tag, TagsResponse>();
        }
    }
}
