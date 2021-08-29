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

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdatePage(PagePayload newPageRequest)
        {
            return Ok(await _pageService.UpdatePage(newPageRequest));
        }

        [HttpDelete("delete/{pageId}")]
        [Authorize]
        public async Task<IActionResult> DeletePage(string pageId)
        {
            return Ok(await _pageService.DeletePage(pageId));
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllPages()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _pageService.GetAllPages(shopId));
        }

        [HttpGet("get/{pageId}")]
        [Authorize]
        public async Task<IActionResult> GetCategory(string pageId)
        {
            return Ok(await _pageService.GetPage(pageId));
        }
    }
}
