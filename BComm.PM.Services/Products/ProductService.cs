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
using BComm.PM.Dto.Images;
using System.Text.RegularExpressions;
using BComm.PM.Services.Common;

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
        private readonly IImageUploadService _imageUploadService;
        private readonly ICategoryQueryService _categoryQueryService;
        private readonly IShopQueryRepository _shopQueryRepository;
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
            IImageUploadService imageUploadService,
            ICategoryQueryService categoryQueryService,
            IShopQueryRepository shopQueryRepository,
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
            _imageUploadService = imageUploadService;
            _categoryQueryService = categoryQueryService;
            _shopQueryRepository = shopQueryRepository;
            _mapper = mapper;
            _env = env;
        }

        public async Task<Response> AddNewProduct(ProductPayload newProductRequest, string shopId)
        {
            try
            {
                var productModel = _mapper.Map<Product>(newProductRequest);
                productModel.HashId = Guid.NewGuid().ToString("N");
                productModel.ShopId = shopId;
                productModel.AddedOn = DateTime.Now;

                var slug = GenerateSlug(productModel.Name);
                var existingSlugs = await _productQueryRepository.GetProductsBySlug(slug, false);
                if (existingSlugs != null && !existingSlugs.Any())
                {
                    productModel.Slug = slug;
                }
                else
                {
                    productModel.Slug = slug + "-" + (existingSlugs.Count() + 1).ToString();
                }

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
                        productModel.StockQuantity = newProductRequest.StockQuantity;

                        if (!string.IsNullOrEmpty(newProductRequest.Image))
                        {
                            var productImage = new ImageInfo(newProductRequest.Image, productModel.HashId, _env);
                            imageModel.HashId = existingProductModel.ImageUrl;
                            await UpdateImages(productImage, imageModel);
                        }

                        await _productCommandsRepository.Update(productModel);
                        await UpdateTags(newProductRequest.Tags, productModel.HashId);

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

        public async Task<Response> GetProductsByCategory(string shopId, string catSlug, string sortBy)
        {
            var sortCol = "Price";
            var sortDirection = "asc";

            if (sortBy.Equals("price_high_low"))
            {
                sortDirection = "desc";
            }

            if (sortBy.Equals("newest"))
            {
                sortCol = "AddedOn";
                sortDirection = "asc";
            }

            try
            {
                var catModel = await _categoryQueryService.GetCategoryBySlug(catSlug, shopId);

                if (catModel != null)
                {
                    var productModels = await _productQueryRepository.GetProducts(shopId, catModel.TagHashId, sortCol, sortDirection);
                    var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(productModels).ToList();

                    foreach (var productResponse in productResponses)
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
                else
                {
                    return new Response()
                    {
                        Message = "Invalid slug",
                        IsSuccess = false
                    };
                }
                
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

        public async Task<Response> GetStockHealth(string shopId)
        {
            try
            {
                var shopModel = await _shopQueryRepository.GetShopById(shopId);

                if(shopModel != null)
                {
                    var productModels = await _productQueryRepository.GetOutOfStockProducts(shopId, shopModel.ReorderLevel);
                    var productStockResponses = new ProductStockResponse()
                    {
                        OutOfStock = _mapper.Map<IEnumerable<ProductResponse>>(productModels.Where(x => x.StockQuantity == 0)),
                        Warning = _mapper.Map<IEnumerable<ProductResponse>>(productModels.Where(
                            x => x.StockQuantity > 0 && x.StockQuantity <= shopModel.ReorderLevel))
                    };

                    return new Response()
                    {
                        Data = productStockResponses,
                        IsSuccess = true
                    };
                }
                else
                {
                    return new Response()
                    {
                        Message = "Shop config doesn't exist",
                        IsSuccess = false
                    };
                }
                
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

        public async Task<Response> GetAllProducts(string shopId, string tagId, string sortBy)
        {
            var sortCol = "Price";
            var sortDirection = "asc";

            if (sortBy.Equals("price_high_low"))
            {
                sortDirection = "desc";
            }

            if (sortBy.Equals("newest"))
            {
                sortCol = "AddedOn";
                sortDirection = "asc";
            }

            try
            {
                var productModels = await _productQueryRepository.GetProducts(shopId, tagId, sortCol, sortDirection);
                var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(productModels).ToList();

                foreach (var productResponse in productResponses)
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
            catch (Exception e)
            {
                return new Response()
                {
                    Message = "Error: " + e.Message,
                    IsSuccess = false
                };
            }

            
        }

        public async Task<Response> SearchProducts(string q, string shopId)
        {
            var productModels = await _productQueryRepository.GetProductsByKeywords(q, shopId);
            var productResponses = _mapper.Map<IEnumerable<ProductResponse>>(productModels).ToList();

            foreach (var productResponse in productResponses)
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
                productResponse.Images = await GetImages(productId);

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
            try
            {
                var existingProductModel = await _productQueryRepository.GetProductById(productId, false);

                if (existingProductModel != null)
                {
                    var galleryImages = await _imagesQueryRepository.GetImageGallery(productId);

                    try
                    {
                        foreach (var galimg in galleryImages)
                        {
                            await DeleteImage(galimg);
                            await _imagesQueryRepository.DeleteGalleryImageByImageId(galimg.HashId, productId);
                        }

                        await _tagsQueryRepository.DeleteTagsByProductId(productId);
                        await _productCommandsRepository.Delete(existingProductModel);

                        return new Response()
                        {
                            Message = "Product Deleted Successfully",
                            IsSuccess = true
                        };
                    }
                    catch(Exception e)
                    {
                        return new Response()
                        {
                            Message = "Couldn't delete the product: " + e.Message,
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

        public async Task<Response> GetImageGallery(string productId)
        {
            var images = await GetImages(productId);
            var productDetails = await _productQueryRepository.GetProductById(productId, false);

            images.ToList().ForEach(x =>
            {
                if(productDetails.ImageUrl == x.Id)
                {
                    x.IsDefault = true;
                }
            });

            return new Response()
            {
                Data = images,
                IsSuccess = true
            };
        }

        public async Task<Response> AddGalleryImage(GalleryImageRequest imageUploadRequest)
        {
            var productImage = new ImageInfo(imageUploadRequest.Image, Guid.NewGuid().ToString("N"), _env);

            var imageModel = await AddImages(productImage);

            var galleryItemId = Guid.NewGuid().ToString("N");

            await _imageGalleryCommandsRepository.Add(new ImageGalleryItem()
            {
                ImageId = imageModel.HashId,
                ProductId = imageUploadRequest.ProductId,
                HashId = galleryItemId
            });

            return new Response()
            {
                Data = galleryItemId,
                IsSuccess = true
            };
        }

        public async Task<Response> DeleteGalleryImage(string imageId, string productId)
        {
            var imageModel = await _imagesQueryRepository.GetImage(imageId);
            if (imageModel != null)
            {
                await DeleteImage(imageModel);
                await _imagesQueryRepository.DeleteGalleryImageByImageId(imageId, productId);

                return new Response()
                {
                    Message = "Image Deleted Successfully",
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

        private async Task<IEnumerable<ImageResponse>> GetImages(string productId)
        {
            var images = _mapper.Map<IEnumerable<ImageResponse>>(
                await _imagesQueryRepository.GetImageGallery(productId)
            );

            return images;
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

        public async Task<Image> UpdateImages(ImageInfo newProductImage, Image oldImage)
        {
            await _imageUploadService.DeleteImages(oldImage);
            var uploadedImageInfo = await _imageUploadService.UploadImage(newProductImage);

            var imageModel = oldImage;
            imageModel.OriginalImage = uploadedImageInfo.OriginalImage;
            imageModel.ThumbnailImage = uploadedImageInfo.ThumbnailImage;

            await _imagesCommandsRepository.Update(imageModel);

            return imageModel;
        }

        private string GenerateSlug(string title)
        {
            string str = title.ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens
            
            return str;
        }
    }
}
