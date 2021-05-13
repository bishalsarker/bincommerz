using BComm.PM.Repositories.Common;
using BComm.PM.Services.Mappings;
using BComm.PM.Services.Orders;
using BComm.PM.Services.Products;
using BComm.PM.Services.Tags;
using Microsoft.Extensions.DependencyInjection;

namespace BComm.PM.Services.Configurations
{
    public static class ServiceConfigurations
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TagMappings), typeof(ProductMappings), typeof(OrderMappings));

            // Services
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
