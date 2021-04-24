using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace BComm.PM.Services.Helpers
{
    public class ImageUploader
    {
        public string FileName
        {
            get
            {
                return _imageInfo.Name + "_main" + _imageInfo.Extention;
            }
        }

        private readonly ImageInfo _imageInfo;

        public ImageUploader(ImageInfo imageInfo)
        {
            _imageInfo = imageInfo;
        }

        public async Task UploadAsync()
        {
            await File.WriteAllBytesAsync(Path.Combine(_imageInfo.Directory, FileName), _imageInfo.FileBytes);
        }
    }
}
