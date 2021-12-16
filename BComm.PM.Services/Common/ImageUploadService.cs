using BComm.PM.Models.Images;
using BComm.PM.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
        private readonly string _blobContainer;

        public ImageUploadService(
            IAzureBlobStorageService azureBlobStorageService,
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            _azureBlobStorageService = azureBlobStorageService;
            _blobContainer = configuration.GetSection("AzureSettings:ImagesContainer").Value;
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
                await _azureBlobStorageService.UploadFileAsync(_blobContainer, mainImageFileName, mainImage);

                using var thumbImage = File.OpenRead(thumbImagePath);
                await _azureBlobStorageService.UploadFileAsync(_blobContainer, thumbnailGenerator.FileName, thumbImage);
            }

            var imageModel = new Image()
            {
                Directory = "/" + _blobContainer + "/",
                OriginalImage = mainImageFileName,
                ThumbnailImage = thumbnailGenerator.FileName,
                HashId = Guid.NewGuid().ToString("N")
            };


            return imageModel;
        }

        public async Task DeleteImages(Image image)
        {
            try
            {
                await _azureBlobStorageService.DeleteFileAsync(_blobContainer, image.OriginalImage);
                await _azureBlobStorageService.DeleteFileAsync(_blobContainer, image.ThumbnailImage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
