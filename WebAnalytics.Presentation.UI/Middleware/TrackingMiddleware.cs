using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.UI.Middleware
{
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly static int _timeout = 30;
        public static ConcurrentDictionary<string, OnlineUserViewModel> OnlineUsers { get; set; } = new ConcurrentDictionary<string, OnlineUserViewModel>();

        public TrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMemoryCache memoryCache)
        {
            var user = new OnlineUserViewModel()
            {
                Ip = context.Connection.RemoteIpAddress.ToString(),
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                LastActivity = DateTime.Now
            };
            OnlineUsers.AddOrUpdate(context.Session.Id, user, (key, value) => { value.LastActivity = DateTime.Now; return value; });
            foreach (var item in OnlineUsers)
            {
                if ((DateTime.Now - item.Value.LastActivity).Seconds >= _timeout)
                {
                    OnlineUsers.Remove(item.Key, out OnlineUserViewModel deletedUser);
                }
            }

            //if (!memoryCache.TryGetValue(context.Session.Id, out string userInfo))
            //{
            //    memoryCache.GetOrCreate(context.Session.Id, entry =>
            //    {
            //        Interlocked.Increment(ref SessionCount);
            //        entry.SlidingExpiration = TimeSpan.FromSeconds(30);
            //        entry.SetPriority(CacheItemPriority.High);
            //        entry.RegisterPostEvictionCallback(Callback);
            //        var ip = context.Connection.RemoteIpAddress;
            //        var userAgent = context.Request.Headers["User-Agent"].ToString();
            //        return $"{ip} - {userAgent}";
            //    });
            //}
            await _next(context);
        }

        //public static void Callback(object key, object value, EvictionReason reason, object state)
        //{
        //    Interlocked.Decrement(ref SessionCount);
        //}
    }
}
