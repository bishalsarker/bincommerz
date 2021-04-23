using AutoMapper;
using BComm.PM.Dto.Common;
using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Products;
using BComm.PM.Models.Tags;
using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using BComm.PM.Services.Helpers;
using System.Collections.Generic;

namespace BComm.PM.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ICommandsRepository<Product> _productCommandsRepository;
        private readonly ICommandsRepository<ProductTags> _productTagsCommandsRepository;
        private readonly ITagsQueryRepository _tagsQueryRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public ProductService(
            ICommandsRepository<Product> productCommandsRepository,
            ICommandsRepository<ProductTags> productTagsCommandsRepository,
            ITagsQueryRepository tagsQueryRepository,
            IMapper mapper,
            IHostingEnvironment env)
        {
            _productCommandsRepository = productCommandsRepository;
            _productTagsCommandsRepository = productTagsCommandsRepository;
            _tagsQueryRepository = tagsQueryRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<Response> AddNewProduct(ProductPayload newProductRequest)
        {
            try
            {
                var productModel = _mapper.Map<Product>(newProductRequest);
                productModel.HashId = Guid.NewGuid().ToString("N");
                productModel.ShopId = "vbt_xyz";

                var imageUploader = new ImageUploader(newProductRequest.Image, productModel.HashId, _env);

                await _productCommandsRepository.Add(productModel);

                await AddTags(newProductRequest.Tags, productModel.HashId);

                await imageUploader.UploadAsync();
                productModel.ImageUrl = imageUploader.ImageUrl;

                await _productCommandsRepository.Update(productModel);

                return new Response()
                {
                    Data = new { id = productModel.HashId },
                    Message = "Product Created Successfully",
                    IsSuccess = true
                };
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

        private async Task AddTags(IEnumerable<string> tags, string productHashId)
        {
            foreach (var tagHashId in tags)
            {
                var tagModel = _tagsQueryRepository.GetTag(tagHashId);

                if (tagModel != null)
                {
                    await _productTagsCommandsRepository.Add(new ProductTags()
                    {
                        TagHashId = tagHashId,
                        ProductHashId = productHashId
                    });
                }
            }
        }
    }
}
