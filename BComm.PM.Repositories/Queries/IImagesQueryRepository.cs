using BComm.PM.Models.Images;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IImagesQueryRepository
    {
        Task DeleteImagesByProductId(string productId);
        Task<Image> GetImage(string imageId);
    }
}