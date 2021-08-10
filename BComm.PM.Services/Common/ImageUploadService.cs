using BComm.PM.Models.Images;
using BComm.PM.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Common
{
    public class ImageUploadService : IImageUploadService
    {
        private readonly IAzureBlobStorageService _azureBlobStorageService;
        private readonly IHostingEnvironment _env;

        public ImageUploadService(
            IAzureBlobStorageService azureBlobStorageService,
            IHostingEnvironment env)
        {
            _azureBlobStorageService = azureBlobStorageService;
            _env = env;
        }

        public async Task<Image> UploadImage(ImageInfo imageInfo)
        {
            var thumbnailGenerator = new ThumbnailGenerator(imageInfo);
            thumbnailGenerator.Generate();

            var directory = Path.Combine(_env.WebRootPath, "images");
            var thumbImagePath = Path.Combine(directory, thumbnailGenerator.FileName);

            var mainImageFileName = imageInfo.Name + "_main" + imageInfo.Extention;
            if (File.Exists(thumbImagePath))
            {
                using var mainImage = new MemoryStream(imageInfo.FileBytes);
                await _azureBlobStorageService.UploadFileAsync("images", mainImageFileName, mainImage);

                using var thumbImage = File.OpenRead(thumbImagePath);
                await _azureBlobStorageService.UploadFileAsync("images", thumbnailGenerator.FileName, thumbImage);
            }

            var imageModel = new Image()
            {
                Directory = "/images/",
                OriginalImage = mainImageFileName,
                ThumbnailImage = thumbnailGenerator.FileName,
                HashId = Guid.NewGuid().ToString("N")
            };


            return imageModel;
        }

        public async Task DeleteImages(Image image)
        {
            await _azureBlobStorageService.DeleteFileAsync("images", image.OriginalImage);
            await _azureBlobStorageService.DeleteFileAsync("images", image.ThumbnailImage);
        }
    }
}
