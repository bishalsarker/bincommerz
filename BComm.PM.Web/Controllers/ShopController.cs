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
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShopController(
            ITagService tagService, 
            IProductService productService,
            IOrderService orderService,
            IHttpContextAccessor httpContextAccessor)
        {
            _tagService = tagService;
            _productService = productService;
            _orderService = orderService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTags([FromHeader] string shop_id)
        {
            return Ok(await _tagService.GetTags(shop_id));
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllProducts([FromQuery] FilterQuery filterQuery, [FromHeader] string shop_id)
        {
            return Ok(await _productService.GetAllProducts(shop_id, filterQuery.TagId, filterQuery.SortBy));
        }

        [HttpGet("products/search")]
        public async Task<IActionResult> SearchProducts(string q, [FromHeader] string shop_id)
        {
            return Ok(await _productService.SearchProducts(q, shop_id));
        }

        [HttpGet("products/{productId}")]
        public async Task<IActionResult> GetProductById(string productId)
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
