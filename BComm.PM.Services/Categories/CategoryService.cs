using AutoMapper;
using BComm.PM.Dto;
using BComm.PM.Dto.Categories;
using BComm.PM.Dto.Common;
using BComm.PM.Models.Categories;
using BComm.PM.Models.Images;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using BComm.PM.Services.Common;
using BComm.PM.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICommandsRepository<Category> _commandsRepository;
        private readonly ICategoryQueryService _categoryQueryRepository;
        private readonly ICommandsRepository<Image> _imagesCommandsRepository;
        private readonly IImagesQueryRepository _imagesQueryRepository;
        private readonly IImageUploadService _imageUploadService;
        private readonly IHostingEnvironment _env;
        private readonly IMapper _mapper;

        public CategoryService(
            ICommandsRepository<Category> commandsRepository,
            ICategoryQueryService categoryQueryRepository,
            ICommandsRepository<Image> imagesCommandsRepository,
            IImagesQueryRepository imagesQueryRepository,
            IImageUploadService imageUploadService,
            IHostingEnvironment env,
            IMapper mapper)
        {
            _commandsRepository = commandsRepository;
            _categoryQueryRepository = categoryQueryRepository;
            _imagesCommandsRepository = imagesCommandsRepository;
            _imagesQueryRepository = imagesQueryRepository;
            _imageUploadService = imageUploadService;
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

                var lastAddedCategory = await _categoryQueryRepository.GetLastAddedCategory(shopId);

                if (lastAddedCategory == null)
                {
                    catModel.OrderNumber = 1;
                } else
                {
                    catModel.OrderNumber = lastAddedCategory.OrderNumber + 1;
                }

                var existingCategoryModel = await _categoryQueryRepository.GetCategoryBySlug(catModel.Slug, shopId);

                if (existingCategoryModel == null)
                {
                    if (catModel.ParentCategoryId != null)
                    {
                        catModel.ImageId = "";
                        await _commandsRepository.Add(catModel);
                    }
                    else
                    {
                        if (newCategoryRequest.Image != null)
                        {
                            var productImage = new ImageInfo(newCategoryRequest.Image, Guid.NewGuid().ToString("N"), _env);
                            var imageModel = await AddImages(productImage);
                            catModel.ImageId = imageModel.HashId;
                            await _commandsRepository.Add(catModel);
                        }
                        else
                        {
                            throw new Exception("Image is required");
                        }
                    }

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

                if (existingImageModel != null)
                {
                    await DeleteImage(existingImageModel);

                    var catImage = new ImageInfo(newCategoryRequest.Image, Guid.NewGuid().ToString("N"), _env);
                    var imageModel = await AddImages(catImage);
                    existingCategoryModel.ImageId = imageModel.HashId;
                }

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

        public async Task<Response> UpdateCategoryOrder(List<CategoryOrderPayload> categoryOrderUpdateRequest)
        {
            try
            {
                foreach (var category in categoryOrderUpdateRequest)
                {
                    var existingCategoryModel = await _categoryQueryRepository.GetCategory(category.Id);
                    if (existingCategoryModel != null)
                    {
                        existingCategoryModel.OrderNumber = category.Order;
                        await _commandsRepository.Update(existingCategoryModel);
                    }
                }

                return new Response()
                {
                    Message = "Category Orders Updated Successfully",
                    IsSuccess = true
                };
            }
            catch(Exception ex)
            {
                return new Response()
                {
                    Message = "Category Orders Couldn't Be Updated",
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> DeleteCategory(string categoryId)
        {
            var existingCategoryModel = await _categoryQueryRepository.GetCategory(categoryId);

            if (existingCategoryModel != null)
            {
                var categories = await _categoryQueryRepository.GetParentCategories(existingCategoryModel.ShopId);
                var sortedCategories = categories
                    .OrderBy(x => x.OrderNumber)
                    .Where(x => x.OrderNumber > existingCategoryModel.OrderNumber)
                    .ToList();

                var order = existingCategoryModel.OrderNumber;
                foreach (var category in sortedCategories)
                {
                    var newCategoryModel = await _categoryQueryRepository.GetCategory(category.HashId);
                    newCategoryModel.OrderNumber = order;
                    await _commandsRepository.Update(newCategoryModel);
                    order++;
                }

                var existingImageModel = await _imagesQueryRepository.GetImage(existingCategoryModel.ImageId);

                if (existingImageModel != null)
                {
                    await DeleteImage(existingImageModel);
                }

                await _categoryQueryRepository.DeleteChildCategories(categoryId);
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
            try
            {
                var categoryModels = await _categoryQueryRepository.GetParentCategories(shopId);

                if (categoryModels.Any(x => x.OrderNumber == 0))
                {
                    categoryModels = await ReorderCategories(categoryModels);
                }
 
                var categoryResponseModels = new List<CategoryResponse>();

                foreach (var categoryModel in categoryModels)
                {
                    var imageModel = await _imagesQueryRepository.GetImage(categoryModel.ImageId);
                    var response = _mapper.Map<CategoryResponse>(categoryModel);

                    if (imageModel != null)
                    {
                        response.ImageUrl = imageModel.Directory + imageModel.ThumbnailImage;
                    }
                    else
                    {
                        response.ImageUrl = "";
                    }
                    
                    categoryResponseModels.Add(response);
                }

                return new Response()
                {
                    Data = categoryResponseModels,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetCategoriesWithSubCategories(string shopId)
        {
            try
            {
                var categoryModels = await _categoryQueryRepository.GetParentCategories(shopId);

                if (categoryModels.Any(x => x.OrderNumber == 0))
                {
                    categoryModels = await ReorderCategories(categoryModels);
                }

                var categoryResponseModels = new List<CategoryResponse>();

                foreach (var categoryModel in categoryModels)
                {
                    var response = _mapper.Map<CategoryResponse>(categoryModel);
                    var subcategories = await GetSubCategories(categoryModel.HashId);
                    response.Subcategories = (subcategories.Data as CategoryResponse).Subcategories;
                    var imageModel = await _imagesQueryRepository.GetImage(categoryModel.ImageId);

                    if (imageModel != null)
                    {
                        response.ImageUrl = imageModel.Directory + imageModel.ThumbnailImage;
                    }
                    else
                    {
                        response.ImageUrl = "";
                    }

                    categoryResponseModels.Add(response);
                }

                return new Response()
                {
                    Data = categoryResponseModels,
                    IsSuccess = true
                };
            }
            catch (Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e.Message,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetCategory(string categoryId)
        {
            var existingCategoryModel = await _categoryQueryRepository.GetCategory(categoryId);

            if (existingCategoryModel != null)
            {
                var response = _mapper.Map<CategoryResponse>(existingCategoryModel);
                var imageModel = await _imagesQueryRepository.GetImage(existingCategoryModel.ImageId);

                if (imageModel != null)
                {
                    response.ImageUrl = imageModel.Directory + imageModel.ThumbnailImage;
                }
                else
                {
                    response.ImageUrl = "";
                }

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
                var subcategories = await GetSubCategories(existingCategoryModel.HashId);
                response.Subcategories = (subcategories.Data as CategoryResponse).Subcategories;
                var imageModel = await _imagesQueryRepository.GetImage(existingCategoryModel.ImageId);
                if (imageModel != null)
                {
                    response.ImageUrl = imageModel.Directory + imageModel.ThumbnailImage;
                }
                else
                {
                    response.ImageUrl = "";
                }

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
            var uploadedImageInfo = await _imageUploadService.UploadImage(productImage);
            await _imagesCommandsRepository.Add(uploadedImageInfo);

            return uploadedImageInfo;
        }

        private async Task<bool> DeleteImage(Image image)
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

        public async Task<Response> GetSubCategories(string categoryId)
        {
            var existingCategoryModel = await _categoryQueryRepository.GetCategory(categoryId);

            if (existingCategoryModel != null)
            {
                var response = _mapper.Map<CategoryResponse>(existingCategoryModel);
                var subcats = new List<CategoryResponse>();

                var childCategories = await _categoryQueryRepository.GetChildCategories(categoryId);

                foreach (var subcat in childCategories)
                {
                    subcats.Add(_mapper.Map<CategoryResponse>(subcat));
                }

                response.Subcategories = subcats;

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

        private async Task<IEnumerable<Category>> ReorderCategories(IEnumerable<Category> parentCategories)
        {
            var sortedCategories = parentCategories.OrderBy(x => x.CreatedOn);
            var updatedCategories = new List<Category>();
            var i = 1;
            foreach (var category in sortedCategories)
            {
                var newCategoryModel =  await _categoryQueryRepository.GetCategory(category.HashId);
                newCategoryModel.OrderNumber = i;
                await _commandsRepository.Update(newCategoryModel);
                updatedCategories.Add(category);
                i++;
            }

            return updatedCategories;
        }
    }
}
