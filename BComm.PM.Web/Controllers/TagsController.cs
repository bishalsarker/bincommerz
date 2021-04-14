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
            await _tagService.AddNewTag(newTagRequest);
            return Ok();
        }
    }
}
