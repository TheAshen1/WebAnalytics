using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using WebAnalytics.UI;

namespace WebAnalytics.Presentation.UI
{
    internal class ConfigureCookie : IConfigureNamedOptions<CookieAuthenticationOptions>
    {
        public ConfigureCookie()
        {
        }

        public void Configure(string name, CookieAuthenticationOptions options)
        {
            // Only configure the schemes you want
            if (name == Startup.CookieScheme)
            {
                // options.LoginPath = "/someotherpath";
            }
        }

        public void Configure(CookieAuthenticationOptions options)
            => Configure(Options.DefaultName, options);
    }
}
