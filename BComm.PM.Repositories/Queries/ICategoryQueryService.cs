using BComm.PM.Models.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ICategoryQueryService
    {
        Task<IEnumerable<Category>> GetParentCategories(string shopId);
        Task<Category> GetCategory(string categoryId);
        Task<Category> GetCategoryBySlug(string slug, string shopId);
        Task<IEnumerable<Category>> GetChildCategories(string categoryId);
        Task DeleteChildCategories(string categoryId);
    }
}