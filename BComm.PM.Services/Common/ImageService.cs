using BComm.PM.Models.Images;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using BComm.PM.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Common
{
    public class ImageService : IImageService
    {
        private readonly ICommandsRepository<Image> _imagesCommandsRepository;
        private readonly IImageUploadService _imageUploadService;
        private readonly IImagesQueryRepository _imagesQueryRepository;
        private readonly IHostingEnvironment _env;

        public ImageService(
            ICommandsRepository<Image> imagesCommandsRepository,
            IImageUploadService imageUploadService,
            IImagesQueryRepository imagesQueryRepository,
            IHostingEnvironment env)
        {
            _imagesCommandsRepository = imagesCommandsRepository;
            _imageUploadService = imageUploadService;
            _imagesQueryRepository = imagesQueryRepository;
            _env = env;
        }

        public async Task<Image> AddImage(ImageInfo sliderImage)
        {
            var uploadedImageInfo = await _imageUploadService.UploadImage(sliderImage);
            await _imagesCommandsRepository.Add(uploadedImageInfo);

            return uploadedImageInfo;
        }

        public async Task<Image> UpdateImage(string imageId, ImageInfo sliderImage)
        {
            var existingImageModel = await _imagesQueryRepository.GetImage(imageId);

            if (existingImageModel != null)
            {
                await DeleteImage(existingImageModel);
                return await AddImage(sliderImage);
            }
            else
            {
                throw new KeyNotFoundException("Error occured while resolving image");
            }
        }

        public async Task<bool> DeleteImage(Image image)
        {
            try
            {
                await _imageUploadService.DeleteImages(image);
                await _imagesQueryRepository.DeleteImage(image.HashId);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
