using BComm.PM.Models.Images;
using BComm.PM.Services.Helpers;
using System.Threading.Tasks;

namespace BComm.PM.Services.Common
{
    public interface IImageUploadService
    {
        Task DeleteImages(Image image);
        Task<Image> UploadImage(ImageInfo imageInfo);
    }
}