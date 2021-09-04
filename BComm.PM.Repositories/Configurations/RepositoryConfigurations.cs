using BComm.PM.Repositories.Common;
using BComm.PM.Repositories.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BComm.PM.Repositories.Configurations
{
    public static class RepositoryConfigurations
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(ICommandsRepository<>), typeof(CommandsRepository<>));
            services.AddTransient<ITagsQueryRepository, TagsQueryRepository>();
            services.AddTransient<ICategoryQueryService, CategoryQueryService>();
            services.AddTransient<IProductQueryRepository, ProductQueryRepository>();
            services.AddTransient<IImagesQueryRepository, ImagesQueryRepository>();
            services.AddTransient<IOrderQueryRepository, OrderQueryRepository>();
            services.AddTransient<IProcessQueryRepository, ProcessQueryRepository>();
            services.AddTransient<IClientsQueryRepository, ClientsQueryRepository>();
            services.AddTransient<IShopQueryRepository, ShopQueryRepository>();
            services.AddTransient<IPagesQueryRepository, PagesQueryRepository>();
            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
        }
    }
}
