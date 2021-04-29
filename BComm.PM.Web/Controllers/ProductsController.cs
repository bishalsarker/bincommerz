using BComm.PM.Dto.Payloads;
using BComm.PM.Services.Products;
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

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("addnew")]
        public async Task<IActionResult> AddNewProduct(ProductPayload newProductRequest)
        {
            return Ok(await _productService.AddNewProduct(newProductRequest));
        }

        [HttpGet("get/{productId}")]
        public async Task<IActionResult> GetProductById(string productId)
        {
            return Ok(await _productService.GetProductById(productId));
        }

        [HttpGet("get/all/{shopId}")]
        public async Task<IActionResult> GetAllProducts(string shopId)
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct(ProductUpdatePayload newProductRequest)
        {
            return Ok(await _productService.UpdateProduct(newProductRequest));
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            return Ok(await _productService.DeleteProduct(productId));
        }
    }
}
