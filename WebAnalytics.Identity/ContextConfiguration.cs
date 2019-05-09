using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebAnalytics.Misc.Identity
{
    public static class ContextConfiguration
    {
        public static IServiceCollection AddIdentityContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlite(configuration.GetConnectionString("IdentityContext")));
            return services;
        }
    }
}
