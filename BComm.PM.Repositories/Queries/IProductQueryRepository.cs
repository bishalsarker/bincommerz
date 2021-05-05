using BComm.PM.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IProductQueryRepository
    {
        Task<Product> GetProductById(string productId, bool resolveImage);
        Task<Product> GetProductByTag(string tagId);
        Task<IEnumerable<Product>> GetProducts(string shopId, string tagId, string sortCol, string sortOrder);
    }
}