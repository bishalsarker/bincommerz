using BComm.PM.Dto;
using BComm.PM.Dto.Common;
using System.Threading.Tasks;

namespace BComm.PM.Services.Categories
{
    public interface ICategoryService
    {
        Task<Response> AddNewCategory(CategoryPayload newCategoryRequest, string shopId);
        Task<Response> DeleteCategory(string categoryId);
        Task<Response> GetCategories(string shopId);
        Task<Response> GetCategory(string categoryId);
        Task<Response> UpdateCategory(CategoryPayload newCategoryRequest);
    }
}