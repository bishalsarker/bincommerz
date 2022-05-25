using BComm.PM.Dto.Widgets;
using BComm.PM.Services.Common;
using BComm.PM.Services.MethodAttributes;
using BComm.PM.Services.Widgets;
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
    [Route("widgets/sliders/")]
    [ApiController]
    [ServiceFilter(typeof(SubscriptionCheckAttribute))]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SliderController(ISliderService sliderService, IHttpContextAccessor httpContextAccessor)
        {
            _sliderService = sliderService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("addnew")]
        [Authorize]
        public async Task<IActionResult> AddSlider(SliderPayload newSliderRequest)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.AddSlider(newSliderRequest, shopId));
        }

        [HttpPost("slide/addnew")]
        [Authorize]
        public async Task<IActionResult> AddSlide(SliderImagePayload newSlideRequest)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.AddSliderImage(newSlideRequest));
        }

        [HttpPut("slide/update")]
        [Authorize]
        public async Task<IActionResult> UpdateSlide(SliderImageUpdatePayload newSlideRequest)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.UpdateSliderImage(newSlideRequest));
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllSliders()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.GetAllSliders(shopId));
        }

        [HttpGet("get/slider/{sliderId}")]
        [Authorize]
        public async Task<IActionResult> GetSlider(string sliderId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.GetSlider(sliderId));
        }

        [HttpGet("get/slides/{sliderId}")]
        [Authorize]
        public async Task<IActionResult> GetSlides(string sliderId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.GetSlides(sliderId));
        }

        [HttpGet("get/slide/{slideId}")]
        [Authorize]
        public async Task<IActionResult> GetSlide(string slideId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.GetSlide(slideId));
        }

        [HttpDelete("slide/delete/{slideId}")]
        [Authorize]
        public async Task<IActionResult> DeleteSlide(string slideId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.DeleteSlide(slideId));
        }

        [HttpDelete("delete/{sliderId}")]
        [Authorize]
        public async Task<IActionResult> DeleteSlider(string sliderId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.DeleteSlider(sliderId));
        }
    }
}
