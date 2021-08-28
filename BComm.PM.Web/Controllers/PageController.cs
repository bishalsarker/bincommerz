using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Pages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("pages")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PageController(IPageService pageService, IHttpContextAccessor httpContextAccessor)
        {
            _pageService = pageService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddPage(PagePayload newPageRequest)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _pageService.AddPage(newPageRequest, shopId));
        }
    }
}
