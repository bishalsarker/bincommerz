using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Pages;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Pages;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using BComm.PM.Services.Mappings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Pages
{
    public class PageService : IPageService
    {
        private readonly ICommandsRepository<Page> _pagesCommandsRepository;
        private readonly IPagesQueryRepository _pagesQueryRepository;
        private readonly IMapper _mapper;

        public PageService(
            ICommandsRepository<Page> pagesCommandsRepository,
            IPagesQueryRepository pagesQueryRepository,
            IMapper mapper)
        {
            _pagesCommandsRepository = pagesCommandsRepository;
            _pagesQueryRepository = pagesQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> AddPage(PagePayload newPageRequest, string shopId)
        {
            var pageModel = _mapper.Map<Page>(newPageRequest);
            pageModel.HashId = Guid.NewGuid().ToString("N");
            pageModel.ShopId = shopId;
            pageModel.CreatedOn = DateTime.UtcNow;
            await _pagesCommandsRepository.Add(pageModel);

            return new Response()
            {
                Data = pageModel.HashId,
                Message = "Tag Created Successfully",
                IsSuccess = true
            };
        }

        public async Task<Response> GetAllPages(string shopId)
        {
            var navLink = await _pagesQueryRepository.GetByCategory(PageMappings.ResolvePageCategory("navbarlink"), shopId);
            var support = await _pagesQueryRepository.GetByCategory(PageMappings.ResolvePageCategory("support"), shopId);
            var faq = await _pagesQueryRepository.GetByCategory(PageMappings.ResolvePageCategory("faq"), shopId);
            var about = await _pagesQueryRepository.GetByCategory(PageMappings.ResolvePageCategory("about"), shopId);

            var pageModels = new PageListResponse()
            {
                NavLink = _mapper.Map<IEnumerable<PageResponse>>(navLink),
                Support = _mapper.Map<IEnumerable<PageResponse>>(support),
                Faq = _mapper.Map<IEnumerable<PageResponse>>(faq),
                About = _mapper.Map<IEnumerable<PageResponse>>(about)
            };

            return new Response()
            {
                Data = pageModels,
                IsSuccess = true
            };
        }

        public async Task<Response> GetPagesByCategory(string category, string shopId)
        {
            var pageModels = await _pagesQueryRepository.GetByCategory(PageMappings.ResolvePageCategory(category), shopId);

            return new Response()
            {
                Data = _mapper.Map<IEnumerable<PageResponse>>(pageModels),
                IsSuccess = true
            };
        }

        public async Task<Response> GetPageBySlug(string category, string slug, string shopId)
        {
            var pageModel = await _pagesQueryRepository.GetByCategoryAndSlug(PageMappings.ResolvePageCategory(category), slug, shopId);

            if (pageModel != null)
            {
                return new Response()
                {
                    Data = _mapper.Map<PageResponse>(pageModel),
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Page not found",
                    IsSuccess = false
                };
            }

            
        }
    }
}
