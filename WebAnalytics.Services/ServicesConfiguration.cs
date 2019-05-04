using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAnalytics.DAL.Repositories;
using WebAnalytics.Services;
using WebAnalytics.Services.Interfaces;

namespace WebAnalytics.BLL.Services
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddScoped<IClientActionService, ClientActionService>();
            return services;
        }
    }
}
