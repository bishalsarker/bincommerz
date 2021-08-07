using AutoMapper;
using BComm.PM.Dto;
using BComm.PM.Dto.Categories;
using BComm.PM.Dto.Common;
using BComm.PM.Models.Categories;
using BComm.PM.Models.Images;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using BComm.PM.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BComm.PM.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICommandsRepository<Category> _commandsRepository;
        private readonly ICategoryQueryService _categoryQueryRepository;
        private readonly ICommandsRepository<Image> _imagesCommandsRepository;
        private readonly IImagesQueryRepository _imagesQueryRepository;
        private readonly IHostingEnvironment _env;
        private readonly IMapper _mapper;

        public CategoryService(
            ICommandsRepository<Category> commandsRepository,
            ICategoryQueryService categoryQueryRepository,
            ICommandsRepository<Image> imagesCommandsRepository,
            IImagesQueryRepository imagesQueryRepository,
            IHostingEnvironment env,
            IMapper mapper)
        {
            _commandsRepository = commandsRepository;
            _categoryQueryRepository = categoryQueryRepository;
            _imagesCommandsRepository = imagesCommandsRepository;
            _imagesQueryRepository = imagesQueryRepository;
            _env = env;
            _mapper = mapper;
        }

        public async Task<Response> AddNewCategory(CategoryPayload newCategoryRequest, string shopId)
        {
            try
            {
                var catModel = _mapper.Map<Category>(newCategoryRequest);
                catModel.HashId = Guid.NewGuid().ToString("N");
                catModel.ShopId = shopId;
                catModel.ParentCategoryId = newCategoryRequest.ParentCategoryId;

                var existingCategoryModel = await _categoryQueryRepository.GetCategoryBySlug(catModel.Slug, shopId);

                if (existingCategoryModel == null)
                {
                    var productImage = new ImageInfo(newCategoryRequest.Image, Guid.NewGuid().ToString("N"), _env);
                    var imageModel = await AddImages(productImage);
                    catModel.ImageId = imageModel.HashId;

                    await _commandsRepository.Add(catModel);

                    return new Response()
                    {
                        Data = _mapper.Map<CategoryResponse>(catModel),
                        Message = "Category Created Successfully",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Slug already exists",
                        IsSuccess = false
                    };
                }
                
            }
            catch(Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> UpdateCategory(CategoryPayload newCategoryRequest)
        {
            var existingCategoryModel = await _categoryQueryRepository.GetCategory(newCategoryRequest.Id);

            if (existingCategoryModel != null)
            {
                existingCategoryModel.HashId = newCategoryRequest.Id;
                existingCategoryModel.Name = newCategoryRequest.Name;
                existingCategoryModel.Description = newCategoryRequest.Description;
                existingCategoryModel.TagHashId = newCategoryRequest.TagHashId;

                var existingImageModel = await _imagesQueryRepository.GetImage(existingCategoryModel.ImageId);
                await DeleteImage(existingImageModel);

                var catImage = new ImageInfo(newCategoryRequest.Image, Guid.NewGuid().ToString("N"), _env);
                var imageModel = await AddImages(catImage);
                existingCategoryModel.ImageId = imageModel.HashId;

                await _commandsRepository.Update(existingCategoryModel);

                return new Response()
                {
                    Data = _mapper.Map<CategoryResponse>(existingCategoryModel),
                    Message = "Category Updated Successfully",
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Category Doesn't Exist",
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> DeleteCategory(string categoryId)
        {
            var existingCategoryModel = await _categoryQueryRepository.GetCategory(categoryId);

            if (existingCategoryModel != null)
            {
                var existingImageModel = await _imagesQueryRepository.GetImage(existingCategoryModel.ImageId);
                await DeleteImage(existingImageModel);

                await _commandsRepository.Delete(existingCategoryModel);

                return new Response()
                {
                    Message = "Category Deleted Successfully",
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Category Doesn't Exist",
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetCategories(string shopId)
        {
            var categoryModels = await _categoryQueryRepository.GetCategories(shopId);
            return new Response()
            {
                Data = _mapper.Map<IEnumerable<CategoryResponse>>(categoryModels),
                IsSuccess = true
            };
        }

        public async Task<Response> GetCategory(string categoryId)
        {
            var existingCategoryModel = await _categoryQueryRepository.GetCategory(categoryId);

            if (existingCategoryModel != null)
            {
                var response = _mapper.Map<CategoryResponse>(existingCategoryModel);
                var imageModel = await _imagesQueryRepository.GetImage(existingCategoryModel.ImageId);
                response.ImageUrl = imageModel.Directory + imageModel.ThumbnailImage;

                return new Response()
                {
                    Data = response,
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Category Doesn't Exist",
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetCategoryBySlug(string slug, string shopId)
        {
            var existingCategoryModel = await _categoryQueryRepository.GetCategoryBySlug(slug, shopId);

            if (existingCategoryModel != null)
            {
                var response = _mapper.Map<CategoryResponse>(existingCategoryModel);
                var imageModel = await _imagesQueryRepository.GetImage(existingCategoryModel.ImageId);
                response.ImageUrl = imageModel.Directory + imageModel.ThumbnailImage;

                return new Response()
                {
                    Data = response,
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Category Doesn't Exist",
                    IsSuccess = false
                };
            }
        }

        private async Task<Image> AddImages(ImageInfo productImage)
        {
            var imageUploader = new ImageUploader(productImage);
            await imageUploader.UploadAsync();

            var thumbnailGenerator = new ThumbnailGenerator(productImage);
            thumbnailGenerator.Generate();

            var imageModel = new Image()
            {
                Directory = "/images/",
                OriginalImage = imageUploader.FileName,
                ThumbnailImage = thumbnailGenerator.FileName,
                HashId = Guid.NewGuid().ToString("N")
            };

            await _imagesCommandsRepository.Add(imageModel);

            return imageModel;
        }

        private async Task<bool> DeleteImage(Image image)
        {
            try
            {
                var directory = Path.Combine(_env.WebRootPath, "images");
                var existingOriginalImagePath = Path.Combine(directory, image.OriginalImage);
                var existingThumbnailImagePath = Path.Combine(directory, image.ThumbnailImage);

                if (File.Exists(existingOriginalImagePath) && File.Exists(existingThumbnailImagePath))
                {
                    File.Delete(existingOriginalImagePath);
                    File.Delete(existingThumbnailImagePath);

                    await _imagesQueryRepository.DeleteImage(image.HashId);
                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
