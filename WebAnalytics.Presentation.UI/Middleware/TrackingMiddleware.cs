using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAnalytics.DAL.Repositories.Interfaces;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.UI.Middleware
{
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly static int _timeout = 30;
        private readonly static string _key = "Id";

        public static int CookiesCreatedCount { get; set; }
        public static ConcurrentDictionary<string, OnlineUserViewModel> OnlineUsers { get; set; } = new ConcurrentDictionary<string, OnlineUserViewModel>();

        public TrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUniqueUsersCounterRepository uniqueUsersCounter)
        {
            var userCookieId = context.Request.Cookies.ContainsKey(_key);
            if (!userCookieId)
            {
                context.Response.Cookies.Append(_key, Guid.NewGuid().ToString());
                uniqueUsersCounter.Increment();
            }
            var userSessionId = context.Session.GetString(_key);
            if (userSessionId is null)
            {
                userSessionId = Guid.NewGuid().ToString();
                context.Session.SetString(_key, userSessionId);
            }
            var user = new OnlineUserViewModel()
            {
                Ip = context.Connection.RemoteIpAddress.ToString(),
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                LastActivity = DateTime.Now
            };
            OnlineUsers.AddOrUpdate(userSessionId, user, (key, value) => { value.LastActivity = DateTime.Now; return value; });
            foreach (var item in OnlineUsers)
            {
                if ((DateTime.Now - item.Value.LastActivity).Seconds >= _timeout)
                {
                    OnlineUsers.Remove(item.Key, out OnlineUserViewModel deletedUser);
                }
            }
            await _next(context);
        }
    }
}
