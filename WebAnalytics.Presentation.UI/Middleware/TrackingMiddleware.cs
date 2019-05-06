using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebAnalytics.UI.Middleware
{
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;

        public static int SessionCount = 0;

        public TrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMemoryCache memoryCache)
        {
            if (!memoryCache.TryGetValue(context.Session.Id, out string userInfo))
            {
                memoryCache.GetOrCreate(context.Session.Id, entry =>
                {
                    Interlocked.Increment(ref SessionCount);
                    entry.SlidingExpiration = TimeSpan.FromSeconds(30);
                    entry.SetPriority(CacheItemPriority.High);
                    entry.RegisterPostEvictionCallback(Callback);
                    var ip = context.Connection.RemoteIpAddress;
                    var userAgent = context.Request.Headers["User-Agent"].ToString();
                    return $"{ip} - {userAgent}";
                });
            }
            await _next(context);
        }

        public static void Callback(object key, object value, EvictionReason reason, object state)
        {
            Interlocked.Decrement(ref SessionCount);
        }
    }
}
