using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using UAParser;
using Wangkanai.Detection;
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
        public static ConcurrentDictionary<string, OnlineClientViewModel> OnlineClients { get; set; } = new ConcurrentDictionary<string, OnlineClientViewModel>();

        public TrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(
            HttpContext context,
            IDeviceResolver deviceResolver,
            IBrowserResolver browserResolver,
            IClientRepository clientRepository)
        {
            if (!deviceResolver.Device.Crawler)
            {
                Parser uaParser = Parser.GetDefault();
                ClientInfo clientInfo = null;

                Guid clientId = Guid.Empty;
                Guid sessionId = Guid.Empty;

                var cookieId = context.Request.Cookies[_key];
                var isExistingUser = context.Request.Cookies.ContainsKey(_key);
                if (!isExistingUser || !Guid.TryParse(cookieId, out clientId) || clientRepository.Get(clientId) is null)
                {
                    clientId = Guid.NewGuid();
                    var userAgent = deviceResolver.UserAgent.ToString();
                    if(clientInfo is null)
                    {
                        clientInfo = uaParser.Parse(userAgent);
                    }
                    context.Response.Cookies.Append(_key, clientId.ToString());
                    var newClient = new DAL.Entities.Client()
                    {
                        ClientId = clientId,
                        Ip = context.Connection.RemoteIpAddress.ToString(),
                        Device = deviceResolver.Device.Type.ToString(),
                        Browser = browserResolver.Browser.Type.ToString(),
                        BrowserVersion = browserResolver.Browser.Version.ToString(),
                        Platform = clientInfo.OS.Family,
                        PlatformVersion = clientInfo.OS.Major,
                    };
                    clientRepository.Add(newClient);
                }
                if (clientInfo is null)
                {
                    clientInfo = uaParser.Parse(deviceResolver.UserAgent.ToString());
                }
                var onlineClient = new OnlineClientViewModel()
                {
                    ClientId = clientId,
                    Ip = context.Connection.RemoteIpAddress.ToString(),
                    UserAgent = $"Device: {deviceResolver.Device.Type.ToString()}, OS: {clientInfo.OS.Family} - {clientInfo.OS.Major}, Browser: {browserResolver.Browser.Type.ToString()} - {browserResolver.Browser.Version.ToString()}",
                    LastActivity = DateTime.Now
                };
                OnlineClients.AddOrUpdate(clientId.ToString(), onlineClient, (key, value) => { value.LastActivity = DateTime.Now; return value; });
            }
            foreach (var item in OnlineClients)
            {
                if ((DateTime.Now - item.Value.LastActivity).Seconds >= _timeout)
                {
                    OnlineClients.Remove(item.Key, out OnlineClientViewModel deletedUser);
                }
            }
            await _next(context);
        }
    }
}
