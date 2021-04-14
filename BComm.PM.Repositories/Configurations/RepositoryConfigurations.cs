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
        }
    }
}
