using BComm.PM.Models.Products;
using BComm.PM.Services.Products;
using BComm.PM.Services.Tags;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BComm.PM.Web.Controllers
{
    [Route("shop")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IProductService _productService;

        public ShopController(ITagService tagService, IProductService productService)
        {
            _tagService = tagService;
            _productService = productService;
        }

        [HttpGet("tags/{shopId}")]
        public async Task<IActionResult> GetTags(string shopId)
        {
            return Ok(await _tagService.GetTags(shopId));
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllProducts([FromQuery] FilterQuery filterQuery)
        {
            return Ok(await _productService.GetAllProducts("vbt_xyz", filterQuery.TagId, filterQuery.SortBy));
        }

        [HttpGet("products/{productId}/{shopId}")]
        public async Task<IActionResult> GetProductById(string productId, string shopId)
        {
            return Ok(await _productService.GetProductById(productId));
        }
    }
}
