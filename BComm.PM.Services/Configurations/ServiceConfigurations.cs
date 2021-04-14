using BComm.PM.Repositories.Common;
using BComm.PM.Services.Mappings;
using BComm.PM.Services.Tags;
using Microsoft.Extensions.DependencyInjection;

namespace BComm.PM.Services.Configurations
{
    public static class ServiceConfigurations
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(TagMappings));

            // Services
            services.AddScoped<ITagService, TagService>();

            // Repositories
            services.AddTransient(typeof(ICommandsRepository<>), typeof(CommandsRepository<>));
        }
    }
}
