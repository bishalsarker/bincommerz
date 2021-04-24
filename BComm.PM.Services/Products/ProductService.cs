﻿using AutoMapper;
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
using BComm.PM.Models.Images;

namespace BComm.PM.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ICommandsRepository<Product> _productCommandsRepository;
        private readonly ICommandsRepository<ProductTags> _productTagsCommandsRepository;
        private readonly ICommandsRepository<Image> _imagesCommandsRepository;
        private readonly ICommandsRepository<ImageGalleryItem> _imageGalleryCommandsRepository;
        private readonly ITagsQueryRepository _tagsQueryRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public ProductService(
            ICommandsRepository<Product> productCommandsRepository,
            ICommandsRepository<ProductTags> productTagsCommandsRepository,
            ICommandsRepository<Image> imagesCommandsRepository,
            ICommandsRepository<ImageGalleryItem> imageGalleryCommandsRepository,
            ITagsQueryRepository tagsQueryRepository,
            IMapper mapper,
            IHostingEnvironment env)
        {
            _productCommandsRepository = productCommandsRepository;
            _productTagsCommandsRepository = productTagsCommandsRepository;
            _imagesCommandsRepository = imagesCommandsRepository;
            _imageGalleryCommandsRepository = imageGalleryCommandsRepository;
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

                var productImage = new ImageInfo(newProductRequest.Image, productModel.HashId, _env);

                await _productCommandsRepository.Add(productModel);

                await AddTags(newProductRequest.Tags, productModel.HashId);

                var imageModel = await AddImages(productImage);

                productModel.ImageUrl = imageModel.HashId;
                await _productCommandsRepository.Update(productModel);

                await _imageGalleryCommandsRepository.Add(new ImageGalleryItem()
                {
                    ImageId = imageModel.HashId,
                    ProductId = productModel.HashId,
                    HashId = Guid.NewGuid().ToString("N")
                });

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
                    Message = "Error: " + e,
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

        private async Task<Image> AddImages(ImageInfo productImage)
        {
            var imageUploader = new ImageUploader(productImage);
            await imageUploader.UploadAsync();

            var thumbnailGenerator = new ThumbnailGenerator(productImage);
            thumbnailGenerator.Generate();

            var imageModel = new Image()
            {
                Directory = productImage.Directory,
                OriginalImage = imageUploader.FileName,
                ThumbnailImage = thumbnailGenerator.FileName,
                HashId = Guid.NewGuid().ToString("N")
            };

            await _imagesCommandsRepository.Add(imageModel);

            return imageModel;
        }
    }
}
