using BComm.PM.Services.Common;
using BComm.PM.Services.ShopConfig;
using BComm.PM.Services.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("subscriptions")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(
            IHttpContextAccessor httpContextAccessor,
            ISubscriptionService subscriptionService)
        {
            _authService = new AuthService(httpContextAccessor.HttpContext);
            _subscriptionService = subscriptionService;
        }

        [HttpGet("status")]
        [Authorize]
        public async Task<IActionResult> GetSubscriptionStatus()
        {
            return Ok(await _subscriptionService.GetSubscriptionStatus(_authService.GetShopId()));
        }
    }
}
