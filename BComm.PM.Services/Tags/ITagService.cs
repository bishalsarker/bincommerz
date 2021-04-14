using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Tags;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Services.Tags
{
    public interface ITagService
    {
        Task<Response> AddNewTag(TagPayload newTagRequest);
        Task<Response> GetTags(string shopId);
    }
}