using BComm.PM.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IProductQueryRepository
    {
        Task<Product> GetProduct(string tagId);
        Task<IEnumerable<Product>> GetProducts(string shopId);
    }
}