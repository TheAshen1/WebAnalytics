using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
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

        public async Task Invoke(HttpContext context, IMemoryCache memoryCache)
        {
            if (!memoryCache.TryGetValue("Count", out int test))
            {
                memoryCache.Set("Count", 0);
            }
            if (!memoryCache.TryGetValue(context.Session.Id, out string userInfo))
            {
                memoryCache.GetOrCreate(context.Session.Id, entry =>
                {
                    var count = memoryCache.Get<int>("Count");
                    memoryCache.Set("Count", ++count);

                    entry.SlidingExpiration = TimeSpan.FromSeconds(30);
                    entry.RegisterPostEvictionCallback((object key, object value, EvictionReason reason, object state) =>
                    {
                        count = memoryCache.Get<int>("Count") - 1;
                        memoryCache.Set("Count", count >= 0 ? count : 0);
                    });
                    var ip = context.Connection.RemoteIpAddress;
                    var userAgent = context.Request.Headers["User-Agent"].ToString();
                    return $"{ip} - {userAgent}";
                });
            }
            await _next(context);
        }
    }
}
