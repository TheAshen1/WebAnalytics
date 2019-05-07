using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAnalytics.DAL.Repositories.Configuration;
using WebAnalytics.Services;
using WebAnalytics.Services.Interfaces;

namespace WebAnalytics.BLL.Services.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddScoped<IStatisticsService, StatisticsService>();
            return services;
        }
    }
}
