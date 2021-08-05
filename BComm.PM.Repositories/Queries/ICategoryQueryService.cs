using BComm.PM.Models.Categories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface ICategoryQueryService
    {
        Task<IEnumerable<Category>> GetCategories(string shopId);
        Task<Category> GetCategory(string categoryId);
    }
}