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
            services.AddTransient<IProductQueryRepository, ProductQueryRepository>();
            services.AddTransient<IImagesQueryRepository, ImagesQueryRepository>();
            services.AddTransient<IOrderQueryRepository, OrderQueryRepository>();
            services.AddTransient<IProcessQueryRepository, ProcessQueryRepository>();
        }
    }
}
