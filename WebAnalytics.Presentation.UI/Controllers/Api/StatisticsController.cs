using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Misc.Common.Extensions;
using WebAnalytics.Presentation.ViewModels;
using WebAnalytics.Services.Interfaces;
using WebAnalytics.UI.Middleware;

namespace WebAnalytics.UI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _clientActionService;

        public StatisticsController(IStatisticsService clientActionService)
        {
            _clientActionService = clientActionService;
        }

        [HttpPost]
        public ActionResult RegisterAction([FromBody]AddClientActionViewModel action)
        {
            _clientActionService.Add(action, HttpContext.Connection.RemoteIpAddress.ToString());
            return Ok();
        }

        [HttpGet("ClientActions")]
        public ActionResult<List<ClientActionViewModel>> GetAllRegisteredActions()
        {
            var clientActions = _clientActionService.GetAll();
            return clientActions;
        }

        [HttpGet("ClientActionsPage/{page?}")]
        public ActionResult<PagedResult<ClientActionViewModel>> GetClientActionsPage(int? page)
        {
            var pageSize = 5;
            var clientActions = _clientActionService.GetPage(page ?? 1, pageSize);
            return clientActions;
        }

        [HttpGet("PageViews")]
        public ActionResult<List<PageViewCountViewModel>> GetPageViewStatistics()
        {
            var pageViews = _clientActionService.GetPageViewStatistics();
            return pageViews;
        }

        [HttpGet("Clicks")]
        public ActionResult<List<ClickStatisticsViewModel>> GetClickStatistics()
        {
            var clickStatistics = _clientActionService.GetClickStatistics();
            return clickStatistics;
        }

        [HttpGet("Realtime")]
        public ActionResult<RealtimeStatisticsViewModel> GetRealtimeStatistics()
        {
            return new RealtimeStatisticsViewModel()
            {
                OnlineUsers = TrackingMiddleware.OnlineUsers.Select(p => p.Value).ToList()
            };
        }

        [HttpGet("DeviceUsage")]
        public ActionResult<List<DeviceUsageStatisticsViewModel>> GetDeviceUsageStatistics()
        {
            var statistics = _clientActionService.GetDeviceUsageStatistics();
            return statistics;
        }

        [HttpGet("Total")]
        public ActionResult<TotalStatisticsViewModel> GetTotalStatistics()
        {
            var totalStatistics = _clientActionService.GetTotalStatistics();
            return totalStatistics;
        }
    }
}