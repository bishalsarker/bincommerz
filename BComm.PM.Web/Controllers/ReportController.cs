using BComm.PM.Services.MethodAttributes;
using BComm.PM.Services.Reports;
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
    [Route("reports")]
    [ApiController]
    [ServiceFilter(typeof(SubscriptionCheckAttribute))]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportController(IReportService reportService, IHttpContextAccessor httpContextAccessor)
        {
            _reportService = reportService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("ordersummary")]
        [Authorize]
        public async Task<IActionResult> GetOrderSummary()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _reportService.GetOrderSummary(shopId));
        }

        [HttpGet("mostorderedproducts")]
        [Authorize]
        public async Task<IActionResult> GetMostOrderedProducts(int month, int year)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _reportService.GetMostOrderedProducts(shopId, month, year));
        }

        [HttpGet("mostpopulartags")]
        [Authorize]
        public async Task<IActionResult> GetMostPopularTags(int month, int year)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _reportService.GetMostPopularTags(shopId, month, year));
        }


    }
}
