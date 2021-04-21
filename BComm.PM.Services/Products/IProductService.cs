using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using System.Threading.Tasks;

namespace BComm.PM.Services.Products
{
    public interface IProductService
    {
        Task<Response> AddNewProduct(ProductPayload newProductRequest);
    }
}