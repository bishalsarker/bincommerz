using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Products;
using BComm.PM.Services.Orders;
using BComm.PM.Services.Products;
using BComm.PM.Services.Tags;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("_myAllowSpecificOrigins")]
    public class ShopController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public ShopController(
            ITagService tagService, 
            IProductService productService,
            IOrderService orderService)
        {
            _tagService = tagService;
            _productService = productService;
            _orderService = orderService;
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

        [HttpGet("products/search")]
        public async Task<IActionResult> SearchProducts(string q)
        {
            return Ok(await _productService.SearchProducts(q));
        }

        [HttpGet("products/{productId}/{shopId}")]
        public async Task<IActionResult> GetProductById(string productId, string shopId)
        {
            return Ok(await _productService.GetProductById(productId));
        }

        [HttpPost("order/addnew")]
        public async Task<IActionResult> AddNewOrder(OrderPayload newOrderRequest, [FromHeader] string shop_id)
        {
            return Ok(await _orderService.AddNewOrder(newOrderRequest, shop_id));
        }

        [HttpGet("order/track/{order_id}")]
        public async Task<IActionResult> TrackOrder(string order_id)
        {
            return Ok(await _orderService.TrackOrder(order_id));
        }
    }
}
