using BComm.PM.Dto.Template;
using BComm.PM.Services.Common;
using BComm.PM.Services.Templates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("templates")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        private readonly AuthService _authService;

        public TemplateController(ITemplateService templateService, IHttpContextAccessor httpContextAccessor)
        {
            _templateService = templateService;
            _authService = new AuthService(httpContextAccessor.HttpContext);
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetTemplates()
        {
            return Ok(await _templateService.GetAllTemplates( _authService.GetShopId()));
        }

        [HttpGet("get/{templateId}")]
        [Authorize]
        public async Task<IActionResult> GetTemplates(string templateId)
        {
            return Ok(await _templateService.GetTemplate(templateId));
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddTemplate(TemplatePayload newTemplateUpdateRequest)
        {
            return Ok(await _templateService.AddTemplate(newTemplateUpdateRequest, _authService.GetShopId()));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateTemplate(TemplateUpdatePayload newTemplateUpdateRequest)
        {
            return Ok(await _templateService.UpdateTemplate(newTemplateUpdateRequest));
        }
    }
}
