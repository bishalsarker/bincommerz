using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Common;
using BComm.PM.Services.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly AuthService _authService;

        public TagsController(ITagService tagService, IHttpContextAccessor httpContextAccessor)
        {
            _tagService = tagService;
            _authService = new AuthService(httpContextAccessor.HttpContext);
        }

        [HttpPost("addnew")]
        [Authorize]
        public async Task<IActionResult> AddNewTag(TagPayload newTagRequest)
        {
            return Ok(await _tagService.AddNewTag(newTagRequest));
        }

        [HttpGet("get/{tagId}")]
        [Authorize]
        public async Task<IActionResult> GetTag(string tagId)
        {
            return Ok(await _tagService.GetTag(tagId));
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetTags()
        {
            return Ok(await _tagService.GetTags(_authService.GetShopId()));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateTag(TagPayload newTagRequest)
        {
            return Ok(await _tagService.UpdateTag(newTagRequest));
        }

        [HttpDelete("delete/{tagId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTag(string tagId)
        {
            return Ok(await _tagService.DeleteTag(tagId));
        }
    }
}
