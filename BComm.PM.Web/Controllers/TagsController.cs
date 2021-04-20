using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Tags;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("addnew")]
        public async Task<IActionResult> AddNewTag(TagPayload newTagRequest)
        {
            return Ok(await _tagService.AddNewTag(newTagRequest));
        }

        [HttpGet("get/{tagId}")]
        public async Task<IActionResult> GetTag(string tagId)
        {
            return Ok(await _tagService.GetTag(tagId));
        }

        [HttpGet("get/all/{shopId}")]
        public async Task<IActionResult> GetTags(string shopId)
        {
            return Ok(await _tagService.GetTags(shopId));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateTag(TagPayload newTagRequest)
        {
            return Ok(await _tagService.UpdateTag(newTagRequest));
        }

        [HttpDelete("delete/{tagId}")]
        public async Task<IActionResult> DeleteTag(string tagId)
        {
            return Ok(await _tagService.DeleteTag(tagId));
        }
    }
}
