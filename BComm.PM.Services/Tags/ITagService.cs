using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Tags;
using System.Threading.Tasks;

namespace BComm.PM.Services.Tags
{
    public interface ITagService
    {
        Task AddNewTag(TagPayload newTagRequest);
    }
}