using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BComm.PM.Dto.Auth;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Auth;
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

        public AuthController(IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
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
        public async Task<IActionResult> GetShopInfo()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();

            return Ok(await _authService.GetShopInfo(shopId));
        }

        [HttpGet("userinfo")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value.ToString();

            return Ok(new { userName = userName });
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
    }
}
