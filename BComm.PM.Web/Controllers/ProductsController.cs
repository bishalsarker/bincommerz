using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Images;
using BComm.PM.Models.Products;
using BComm.PM.Services.Common;
using BComm.PM.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly AuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductsController(IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _authService = new AuthService(httpContextAccessor.HttpContext);
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("addnew")]
        [Authorize]
        public async Task<IActionResult> AddNewProduct(ProductPayload newProductRequest)
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _productService.AddNewProduct(newProductRequest, shopId));
        }

        [HttpGet("get/{productId}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(string productId)
        {
            return Ok(await _productService.GetProductById(productId));
        }

        [HttpGet("stockhealth")]
        public async Task<IActionResult> GetStockHealth()
        {
            var claims = _httpContextAccessor.HttpContext.User.Claims;
            var shopId = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _productService.GetStockHealth(shopId));
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts([FromQuery] FilterQuery filterQuery)
        { 
            return Ok(await _productService.GetAllProducts(
                _authService.GetShopId(), filterQuery.TagId, filterQuery.SortBy, filterQuery.PageSize, filterQuery.PageNumber, filterQuery.Keyword));
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(ProductUpdatePayload newProductRequest)
        {
            return Ok(await _productService.UpdateProduct(newProductRequest));
        }

        [HttpDelete("delete/{productId}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            return Ok(await _productService.DeleteProduct(productId));
        }

        [HttpGet("imagegallery/{productId}")]
        [Authorize]
        public async Task<IActionResult> GetImageGallery(string productId)
        {
            return Ok(await _productService.GetImageGallery(productId));
        }

        [HttpPost("imagegallery/add")]
        [Authorize]
        public async Task<IActionResult> AddImageToGallery(GalleryImageRequest imageUploadRequest)
        {
            return Ok(await _productService.AddGalleryImage(imageUploadRequest));
        }

        [HttpDelete("imagegallery/delete/{productId}/{imageId}")]
        [Authorize]
        public async Task<IActionResult> DeleteImageFromGallery(string productId, string imageId)
        {
            return Ok(await _productService.DeleteGalleryImage(imageId, productId));
        }
    }
}