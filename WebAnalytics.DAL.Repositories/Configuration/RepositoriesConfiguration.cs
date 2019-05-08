using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAnalytics.DAL.Context;
using WebAnalytics.DAL.Repositories.Interfaces;

namespace WebAnalytics.DAL.Repositories.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ITimeOnPageRepository, TimeOnPageRepository>();
            return services;
        }
    }
}
