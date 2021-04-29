using BComm.PM.Models.Images;
using System.Threading.Tasks;

namespace BComm.PM.Repositories.Queries
{
    public interface IImagesQueryRepository
    {
        Task<Image> GetImage(string imageId);
    }
}