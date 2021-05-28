using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Products;
using BComm.PM.Services.Common;
using BComm.PM.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly AuthService _authService;

        public ProductsController(IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _authService = new AuthService(httpContextAccessor.HttpContext);
        }

        [HttpPost("addnew")]
        [Authorize]
        public async Task<IActionResult> AddNewProduct(ProductPayload newProductRequest)
        {
            return Ok(await _productService.AddNewProduct(newProductRequest));
        }

        [HttpGet("get/{productId}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(string productId)
        {
            return Ok(await _productService.GetProductById(productId));
        }

        [HttpGet("get/all")]
        [Authorize]
        public async Task<IActionResult> GetAllProducts([FromQuery] FilterQuery filterQuery)
        { 
            return Ok(await _productService.GetAllProducts(_authService.GetShopId(), filterQuery.TagId, filterQuery.SortBy));
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
    }
}
