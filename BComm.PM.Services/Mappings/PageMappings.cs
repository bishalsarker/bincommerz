using AutoMapper;
using BComm.PM.Dto.Pages;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace BComm.PM.Services.Mappings
{
    public class PageMappings : Profile
    {
        public PageMappings()
        {
            CreateMap<PagePayload, Page>()
                .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => ResolvePageCategory(src.Category)));

            CreateMap<Page, PageResponse>()
                .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => ResolvePageCategory(src.Category)))
                .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.HashId));
        }

        public static PageCategories ResolvePageCategory(string category)
        {
            switch (category)
            {
                case "usefullink":
                    return PageCategories.UsefulLink;
                case "navbarlink":
                    return PageCategories.NavbarLink;
                case "support":
                    return PageCategories.Support;
                case "article":
                    return PageCategories.Article;
                case "about":
                    return PageCategories.About;
                case "faq":
                    return PageCategories.FAQ;
                case "externallink":
                    return PageCategories.ExternalLink;
                default:
                    return PageCategories.Article;
            }
        }

        public static string ResolvePageCategory(PageCategories category)
        {
            switch (category)
            {
                case PageCategories.UsefulLink:
                    return "usefullink";
                case PageCategories.NavbarLink:
                    return "navbarlink";
                case PageCategories.Support:
                    return "support";
                case PageCategories.Article:
                    return "article";
                case PageCategories.About:
                    return "about";
                case PageCategories.FAQ:
                    return "faq";
                case PageCategories.ExternalLink:
                    return "externallink";
                default:
                    return "article";
            }
        }
    }
}
