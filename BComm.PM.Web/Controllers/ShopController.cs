using BComm.PM.Dto.Payloads;
using BComm.PM.Models.Products;
using BComm.PM.Services.Auth;
using BComm.PM.Services.Categories;
using BComm.PM.Services.Coupons;
using BComm.PM.Services.Orders;
using BComm.PM.Services.Pages;
using BComm.PM.Services.Products;
using BComm.PM.Services.ShopConfig;
using BComm.PM.Services.Tags;
using BComm.PM.Services.Templates;
using BComm.PM.Services.Widgets;
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
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICouponService _couponService;
        private readonly IPageService _pageService;
        private readonly ISliderService _sliderService;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITemplateService _templateService;
        private readonly IDeliveryChargeService _deliveryChargeService;
        private readonly IShopConfigService _shopConfigService;

        public ShopController(
            IPageService pageService,
            ISliderService sliderService,
            ICategoryService categoryService,
            IProductService productService,
            IOrderService orderService,
            ICouponService couponService,
            IAuthService authService,
            ITemplateService templateService,
            IHttpContextAccessor httpContextAccessor,
            IDeliveryChargeService deliveryChargeService,
            IShopConfigService shopConfigService)
        {
            _pageService = pageService;
            _sliderService = sliderService;
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _couponService = couponService;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _templateService = templateService;
            _deliveryChargeService = deliveryChargeService;
            _shopConfigService = shopConfigService;
        }

        [HttpGet("config/app_urls")]
        public async Task<IActionResult> GetAppUrls()
        {
            return Ok(await _shopConfigService.GetAppUrls());
        }

        [HttpGet("config/domains")]
        public async Task<IActionResult> GetDomainUrls()
        {
            return Ok(await _shopConfigService.GetDomainUrls());
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetShopInfo([FromHeader] string shop_id)
        {
            return Ok(await _authService.GetShopInfo(shop_id));
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories([FromHeader] string shop_id)
        {
            return Ok(await _categoryService.GetCategoriesWithSubCategories(shop_id));
        }

        [HttpGet("category/{slug}")]
        public async Task<IActionResult> GetCategory(string slug, [FromHeader] string shop_id)
        {
            return Ok(await _categoryService.GetCategoryBySlug(slug, shop_id));
        }

        [HttpGet("products/get")]
        public async Task<IActionResult> GetAllProducts([FromQuery] FilterQuery filterQuery, [FromHeader] string shop_id)
        {
            return Ok(await _productService.GetProductsByCategory(
                shop_id, filterQuery.CatSlug, filterQuery.SortBy, filterQuery.Keyword, filterQuery.PageSize, filterQuery.PageNumber));
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

        [HttpGet("products/similar/{productId}")]
        public async Task<IActionResult> GetSimilarProducts(string productId)
        {
            return Ok(await _productService.GetSimilarProducts(productId));
        }

        [HttpGet("coupon/apply/{couponCode}/{amount}")]
        public async Task<IActionResult> CheckCouponValidity(string couponCode, double amount, [FromHeader] string shop_id)
        {
            return Ok(await _couponService.GetCouponDiscount(couponCode, amount, shop_id));
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

        [HttpGet("orders/delivery-charges")]
        public async Task<IActionResult> GetAllDeliveryCharges([FromHeader] string shop_id)
        {
            return Ok(await _deliveryChargeService.GetAllDeliveryCharges(shop_id));
        }

        [HttpGet("pages/getall")]
        public async Task<IActionResult> AddPage([FromHeader] string shop_id)
        {
            Console.WriteLine("Shop ID:" + shop_id);
            return Ok(await _pageService.GetAllPagesSorted(shop_id));
        }

        [HttpGet("pages/get")]
        public async Task<IActionResult> GetPage(string cat, string slug, [FromHeader] string shop_id)
        {
            return Ok(await _pageService.GetPageBySlug(cat, slug, shop_id));
        }

        [HttpGet("widgets/slider/get/{sliderId}")]
        public async Task<IActionResult> GetSlider(string sliderId)
        {
            return Ok(await _sliderService.GetSliderWithSlides(sliderId));
        }

        [HttpGet("templates/get")]
        public async Task<IActionResult> GetTemplate([FromHeader] string shop_id)
        {
            return Ok(await _templateService.GetDefaultTemplate(shop_id));
        }
    }
}
