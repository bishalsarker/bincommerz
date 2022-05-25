using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Common;
using BComm.PM.Services.Coupons;
using BComm.PM.Services.MethodAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("coupons")]
    [ApiController]
    [ServiceFilter(typeof(SubscriptionCheckAttribute))]
    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly AuthService _authService;

        public CouponController(ICouponService couponService, IHttpContextAccessor httpContextAccessor)
        {
            _couponService = couponService;
            _authService = new AuthService(httpContextAccessor.HttpContext);
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddCoupon(CouponPayload newCouponRequest)
        {
            return Ok(await _couponService.AddNewCoupon(newCouponRequest, _authService.GetShopId()));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateCoupon(CouponUpdatePayload newCouponRequest)
        {
            return Ok(await _couponService.UpdateCoupon(newCouponRequest));
        }

        [HttpDelete("delete/{couponId}")]
        [Authorize]
        public async Task<IActionResult> DeleteCoupon(string couponId)
        {
            return Ok(await _couponService.DeleteCoupon(couponId));
        }

        [HttpGet("get/{couponId}")]
        [Authorize]
        public async Task<IActionResult> GetCoupon(string couponId)
        {
            return Ok(await _couponService.GetCouponById(couponId));
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllPages()
        {
            return Ok(await _couponService.GetAllCoupons(_authService.GetShopId()));
        }
    }
}
