using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Products
{
    public interface IProductService
    {
        Task<Response> AddNewProduct(ProductPayload newProductRequest, string shopId);
        Task<Response> DeleteProduct(string productId);
        Task<Response> GetAllProducts(string shopId, string tagId, string sortBy);
        Task<Response> SearchProducts(string q, string shopId);
        Task<Response> GetProductById(string productId);
        Task<Response> UpdateProduct(ProductUpdatePayload newProductRequest);
    }
}