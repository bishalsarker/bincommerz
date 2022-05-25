using BComm.PM.Services;
using BComm.PM.Services.MethodAttributes;
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
    [Route("processes")]
    [ApiController]
    [ServiceFilter(typeof(SubscriptionCheckAttribute))]
    public class ProcessesController : ControllerBase
    {
        private readonly IProcessService _processService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProcessesController(IProcessService processService, IHttpContextAccessor httpContextAccessor)
        {
            _processService = processService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("getnextprocess/{currentStep}")]
        [Authorize]
        public async Task<IActionResult> GetNextProcess(int currentStep)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _processService.GetNextProcess(shopId, currentStep));
        }
    }
}
