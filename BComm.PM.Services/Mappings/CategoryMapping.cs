using AutoMapper;
using BComm.PM.Dto;
using BComm.PM.Dto.Categories;
using BComm.PM.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryPayload, Category>();

            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId));
        }
    }
}
