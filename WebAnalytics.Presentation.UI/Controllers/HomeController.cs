using System;
using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebAnalytics.Presentation.ViewModels;
using WebAnalytics.UI.Middleware;
using WebAnalytics.UI.Models;

namespace WebAnalytics.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Set("test", Encoding.Unicode.GetBytes("something"));
            return View(new HomeViewModel { UserCount = TrackingMiddleware.OnlineUsers.Count, SessionId = HttpContext.Session.Id, UserInfo = _memoryCache.Get<string>(HttpContext.Session.Id) });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
