using BComm.PM.Models.Images;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IImagesQueryRepository
    {
        Task DeleteByImageId(string imageId, string productId);
        Task DeleteImage(string imageHashId);
        Task DeleteImagesByProductId(string productId);
        Task<Image> GetImage(string imageId);
        Task<IEnumerable<Image>> GetImageGallery(string productId);
    }
}