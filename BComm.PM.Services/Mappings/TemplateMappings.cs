using AutoMapper;
using BComm.PM.Dto.Template;
using BComm.PM.Models.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class TemplateMappings : Profile
    {
        public TemplateMappings()
        {
            CreateMap<TemplatePayload, Template>();

            CreateMap<Template, TemplateResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId));
        }
    }
}
