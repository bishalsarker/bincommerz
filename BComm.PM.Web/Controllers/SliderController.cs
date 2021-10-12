using BComm.PM.Dto.Widgets;
using BComm.PM.Services.Common;
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

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllSliders()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.GetAllSliders(shopId));
        }

        [HttpGet("get/slides/{sliderId}")]
        [Authorize]
        public async Task<IActionResult> GetSlider(string sliderId)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _sliderService.GetSlides(sliderId));
        }
    }
}
