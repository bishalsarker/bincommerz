using AutoMapper;
using BComm.PM.Dto;
using BComm.PM.Dto.Categories;
using BComm.PM.Dto.Common;
using BComm.PM.Models.Categories;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BComm.PM.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICommandsRepository<Category> _commandsRepository;
        private readonly ICategoryQueryService _categoryQueryRepository;
        private readonly IMapper _mapper;

        public CategoryService(
            ICommandsRepository<Category> commandsRepository,
            ICategoryQueryService categoryQueryRepository,
            IMapper mapper)
        {
            _commandsRepository = commandsRepository;
            _categoryQueryRepository = categoryQueryRepository;
            _mapper = mapper;
        }

        public async Task<Response> AddNewCategory(CategoryPayload newCategoryRequest, string shopId)
        {
            var catModel = _mapper.Map<Category>(newCategoryRequest);
            catModel.HashId = Guid.NewGuid().ToString("N");
            catModel.ShopId = shopId;
            catModel.ParentCategoryId = "";
            await _commandsRepository.Add(catModel);

            return new Response()
            {
                Data = _mapper.Map<CategoryResponse>(catModel),
                Message = "Category Created Successfully",
                IsSuccess = true
            };
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
                return new Response()
                {
                    Data = _mapper.Map<CategoryResponse>(existingCategoryModel),
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
    }
}
