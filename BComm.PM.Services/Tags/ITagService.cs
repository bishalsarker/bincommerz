using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Tags;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Services.Tags
{
    public interface ITagService
    {
        Task<Response> AddNewTag(TagPayload newTagRequest, string shopId);

        Task<Response> UpdateTag(TagPayload newTagRequest);

        Task<Response> DeleteTag(string tagId);

        Task<Response> GetTags(string shopId);

        Task<Response> GetTag(string tagId);
    }
}