using BComm.PM.Dto.Common;
using BComm.PM.Dto.Images;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Images;
using System.Collections.Generic;
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
        Task<Response> GetImageGallery(string productId);
        Task<Response> AddGalleryImage(GalleryImageRequest imageUploadRequest);
        Task<Response> DeleteGalleryImage(string imageId, string productId);
        Task<Response> GetProductsByCategory(string shopId, string catSlug, string sortBy);
    }
}