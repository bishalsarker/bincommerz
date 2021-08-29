using BComm.PM.Models.Pages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IPagesQueryRepository
    {
        Task<IEnumerable<Page>> GetAllPages(string shopId);
        Task<IEnumerable<Page>> GetByCategory(PageCategories category, string shopId);
        Task<Page> GetByCategoryAndSlug(PageCategories category, string slug, string shopId);
        Task<Page> GetById(string pageId);
    }
}