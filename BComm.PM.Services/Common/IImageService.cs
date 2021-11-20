using BComm.PM.Models.Images;
using BComm.PM.Services.Helpers;
using System.Threading.Tasks;

namespace BComm.PM.Services.Common
{
    public interface IImageService
    {
        Task<Image> AddImage(ImageInfo sliderImage);
        Task<bool> DeleteImage(Image image);
        Task<Image> UpdateImage(string imageId, ImageInfo sliderImage);
    }
}