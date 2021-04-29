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
using BComm.PM.Models.Images;
using BComm.PM.Dto.Products;
using System.Linq;
using System.IO;

namespace BComm.PM.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ICommandsRepository<Product> _productCommandsRepository;
        private readonly ICommandsRepository<ProductTags> _productTagsCommandsRepository;
        private readonly ICommandsRepository<Image> _imagesCommandsRepository;
        private readonly ICommandsRepository<ImageGalleryItem> _imageGalleryCommandsRepository;
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly ITagsQueryRepository _tagsQueryRepository;
        private readonly IImagesQueryRepository _imagesQueryRepository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public ProductService(
            ICommandsRepository<Product> productCommandsRepository,
            ICommandsRepository<ProductTags> productTagsCommandsRepository,
            ICommandsRepository<Image> imagesCommandsRepository,
            ICommandsRepository<ImageGalleryItem> imageGalleryCommandsRepository,
            IProductQueryRepository productQueryRepository,
            ITagsQueryRepository tagsQueryRepository,
            IImagesQueryRepository imagesQueryRepository,
            IMapper mapper,
            IHostingEnvironment env)
        {
            _productCommandsRepository = productCommandsRepository;
            _productTagsCommandsRepository = productTagsCommandsRepository;
            _imagesCommandsRepository = imagesCommandsRepository;
            _imageGalleryCommandsRepository = imageGalleryCommandsRepository;
            _productQueryRepository = productQueryRepository;
            _tagsQueryRepository = tagsQueryRepository;
            _imagesQueryRepository = imagesQueryRepository;
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

        public async Task<Response> UpdateProduct(ProductUpdatePayload newProductRequest)
        {
            try
            {
                var existingProductModel = await _productQueryRepository.GetProductById(newProductRequest.Id, false);

                if (existingProductModel != null)
                {
                    var imageModel = await _imagesQueryRepository.GetImage(existingProductModel.ImageUrl);

                    if (imageModel != null)
                    {
                        var productModel = existingProductModel;
                        productModel.Name = newProductRequest.Name;
                        productModel.Description = newProductRequest.Description;
                        productModel.Price = newProductRequest.Price;
                        productModel.Discount = newProductRequest.Discount;

                        await _productCommandsRepository.Update(productModel);
                        await UpdateTags(newProductRequest.Tags, productModel.HashId);

                        if(!string.IsNullOrEmpty(newProductRequest.Image))
                        {
                            var productImage = new ImageInfo(newProductRequest.Image, productModel.HashId, _env);
                            imageModel.HashId = existingProductModel.ImageUrl;
                            await UpdateImages(productImage, imageModel);
                        } 

                        return new Response()
                        {
                            Data = new { id = productModel.HashId },
                            Message = "Product Updated Successfully",
                            IsSuccess = true
                        };
                    }
                    else
                    {
                        return new Response()
                        {
                            Message = "Couldn't resolve image",
                            IsSuccess = false
                        };
                    }
                }
                else
                {
                    return new Response()
                    {
                        Message = "Product Doesn't Exist",
                        IsSuccess = false
                    };
                }
            }
            catch (Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e,
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> GetAllProducts()
        {
            var productModels = await _productQueryRepository.GetProducts("vbt_xyz");
            var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(productModels).ToList();

            foreach(var productResponse in productResponses)
            {
                var tags = await _tagsQueryRepository.GetTagsByProductId(productResponse.Id);
                productResponse.Tags = tags.Select(x => x.TagHashId).ToList();
            }

            return new Response()
            {
                Data = _mapper.Map<IEnumerable<ProductResponse>>(productResponses),
                IsSuccess = true
            };
        }

        public async Task<Response> GetProductById(string productId)
        {
            var productModel = await _productQueryRepository.GetProductById(productId, true);

            if(productModel != null)
            {
                var productResponse = _mapper.Map<ProductResponse>(productModel);
                var tags = await _tagsQueryRepository.GetTagsByProductId(productId);
                productResponse.Tags = tags.Select(x => x.TagHashId).ToList();

                return new Response()
                {
                    Data = productResponse,
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Product Doesn't Exist",
                    IsSuccess = false
                };
            }
        }

        public async Task<Response> DeleteProduct(string productId)
        {
            var existingTagModel = await _productQueryRepository.GetProductById(productId, false);

            if (existingTagModel != null)
            {
                await _productCommandsRepository.Delete(existingTagModel);

                return new Response()
                {
                    Message = "Product Deleted Successfully",
                    IsSuccess = true
                };
            }
            else
            {
                return new Response()
                {
                    Message = "Product Doesn't Exist",
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

        private async Task UpdateTags(IEnumerable<string> tags, string productHashId)
        {
            await _tagsQueryRepository.DeleteTagsByProductId(productHashId);
            await AddTags(tags, productHashId);
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

        private async Task<Image> UpdateImages(ImageInfo newProductImage, Image oldImage)
        {
            var directory = Path.Combine(_env.WebRootPath, "images");
            var existingOriginalImagePath = Path.Combine(directory, oldImage.OriginalImage);
            var existingThumbnailImagePath = Path.Combine(directory, oldImage.ThumbnailImage);

            if (File.Exists(existingOriginalImagePath) && File.Exists(existingThumbnailImagePath))
            {
                File.Delete(existingOriginalImagePath);
                File.Delete(existingThumbnailImagePath);
            }

            var imageUploader = new ImageUploader(newProductImage);
            await imageUploader.UploadAsync();

            var thumbnailGenerator = new ThumbnailGenerator(newProductImage);
            thumbnailGenerator.Generate();

            var imageModel = oldImage;
            imageModel.OriginalImage = imageUploader.FileName;
            imageModel.ThumbnailImage = thumbnailGenerator.FileName;

            await _imagesCommandsRepository.Update(imageModel);

            return imageModel;
        }
    }
}
