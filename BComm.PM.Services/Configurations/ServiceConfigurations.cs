﻿using BComm.PM.Repositories.Common;
using BComm.PM.Services.Auth;
using BComm.PM.Services.Mappings;
using BComm.PM.Services.Orders;
using BComm.PM.Services.Products;
using BComm.PM.Services.Reports;
using BComm.PM.Services.Tags;
using Microsoft.Extensions.DependencyInjection;

namespace BComm.PM.Services.Configurations
{
    public static class ServiceConfigurations
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TagMappings), typeof(ProductMappings), typeof(OrderMappings), typeof(ProcessMappings));

            // Services
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderPaymentService, OrderPaymentService>();
            services.AddScoped<IProcessService, ProcessService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IReportService, ReportService>();
        }
    }
}
