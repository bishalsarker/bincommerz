﻿using BComm.PM.Repositories.Common;
using BComm.PM.Services.Admin;
using BComm.PM.Services.Auth;
using BComm.PM.Services.Categories;
using BComm.PM.Services.Common;
using BComm.PM.Services.Coupons;
using BComm.PM.Services.Mappings;
using BComm.PM.Services.MethodAttributes;
using BComm.PM.Services.Orders;
using BComm.PM.Services.Pages;
using BComm.PM.Services.Products;
using BComm.PM.Services.Reports;
using BComm.PM.Services.ShopConfig;
using BComm.PM.Services.Subscriptions;
using BComm.PM.Services.Tags;
using BComm.PM.Services.Templates;
using BComm.PM.Services.Widgets;
using Microsoft.Extensions.DependencyInjection;

namespace BComm.PM.Services.Configurations
{
    public static class ServiceConfigurations
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(TagMappings), 
                typeof(ProductMappings), 
                typeof(OrderMappings), 
                typeof(ProcessMappings),
                typeof(CategoryMapping));

            // Services
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderPaymentService, OrderPaymentService>();
            services.AddScoped<IProcessService, ProcessService>();
            services.AddScoped<IAuthService, Auth.AuthService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IAzureBlobStorageService, AzureBlobStorageService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageUploadService, ImageUploadService>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ITemplateService, TemplateService>();
            services.AddScoped<IDeliveryChargeService, DeliveryChargeService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<IShopConfigService, ShopConfigService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<SubscriptionCheckAttribute>();
        }
    }
}
