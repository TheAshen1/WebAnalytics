using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAnalytics.DAL.Context
{
    public static class ContextConfiguration
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebStatisticsContext>(options => options.UseSqlite(configuration.GetConnectionString("WebStatisticsContext")));
            return services;
        }
    }
}
