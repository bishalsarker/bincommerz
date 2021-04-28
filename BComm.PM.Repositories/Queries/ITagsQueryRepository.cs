using BComm.PM.Models.Tags;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ITagsQueryRepository
    {
        Task<Tag> GetTag(string tagId);

        Task<IEnumerable<Tag>> GetTags(string shopId);
        Task<IEnumerable<ProductTags>> GetTagsByProductId(string productId);
    }
}