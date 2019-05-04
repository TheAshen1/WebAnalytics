using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.Threading.Tasks;

namespace WebAnalytics.UI.Middleware
{
    public class TrackingMiddleware 
    {
        private readonly RequestDelegate _next;

        public TrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (HttpMethods.IsGet(context.Request.Method))
            {
                var requestString = UriHelper.GetDisplayUrl(context.Request);

            }
            await _next(context);
        }
    }
}
