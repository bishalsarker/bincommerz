using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BComm.PM.Dto.Auth;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Dto.UrlMappings;
using BComm.PM.Models.Auth;
using BComm.PM.Services.Auth;
using BComm.PM.Services.MethodAttributes;
using BComm.PM.Services.ShopConfig;
using BComm.PM.Services.Subscriptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BComm.PM.Web.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private readonly IShopConfigService _shopConfigService;
        private readonly ISubscriptionService _subscriptionService;

        public AuthController(
            IHttpContextAccessor httpContextAccessor, 
            IAuthService authService, 
            IShopConfigService shopConfigService,
            ISubscriptionService subscriptionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
            _shopConfigService = shopConfigService;
            _subscriptionService = subscriptionService;
        }

        [HttpPost]
        [Route("createaccount")]
        public async Task<IActionResult> CreateAccount(UserAccountPayload newUserAccountDetails)
        {
            return Ok(await _authService.CreateAccount(newUserAccountDetails));
        }

        [HttpGet("verify")]
        [Authorize]
        public IActionResult VerifyToken()
        {
            return Ok();
        }

        [HttpGet("shopinfo")]
        [Authorize]
        // [ServiceFilter(typeof(SubscriptionCheckAttribute))]
        public async Task<IActionResult> GetShopInfo()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

            return Ok(await _authService.GetShopInfo(shopId));
        }

        [HttpGet("domains")]
        [Authorize]
        public async Task<IActionResult> GetShopDomains()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

            return Ok(await _shopConfigService.GetShopAllUrlMappings(shopId));
        }

        [HttpPost("domains")]
        [Authorize]
        public async Task<IActionResult> AddShopDomain(UrlMappingPayload newDomainRequest)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

            return Ok(await _shopConfigService.AddDomain(newDomainRequest, shopId));
        }

        [HttpDelete("domains/delete/{domainId}")]
        [Authorize]
        public async Task<IActionResult> DeleteShopDomain(string domainId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

            return Ok(await _shopConfigService.DeleteDomain(domainId));
        }

        [HttpPost("domains/app_url")]
        [Authorize]
        public async Task<IActionResult> AddAppUrl()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

            return Ok(await _shopConfigService.AddAppUrl(shopId));
        }

        [HttpGet("userinfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value.ToString();
            var userInfo = (await _authService.GetUserInfo(userName)).Data as User;

            return Ok(new { userName = userName, subscriptionPlan = GetSubscriptionPlan(userInfo.SubscriptionPlan) });
        }

        [HttpPatch("updateshop")]
        [Authorize]
        public async Task<IActionResult> UpdateShopInfo(ShopUpdatePayload shopUpdatePayload)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

            return Ok(await _authService.UpdateShop(shopUpdatePayload, shopId));
        }

        [HttpPatch("updatepassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(PasswordUpdatePayload passwordUpdatePayload)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value.ToString();

            return Ok(await _authService.UpdatePassword(passwordUpdatePayload, userName));
        }

        private string GetSubscriptionPlan(SubscriptionPlans subscriptionPlan)
        {
            switch(subscriptionPlan)
            {
                case SubscriptionPlans.Free:
                    return "free";
                case SubscriptionPlans.Basic:
                    return "basic";
                case SubscriptionPlans.Enterprise:
                    return "enterprise";
                default:
                    return "invalid";
            }
        }
    }
}
