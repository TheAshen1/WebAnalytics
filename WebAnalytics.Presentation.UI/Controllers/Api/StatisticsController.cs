using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsServer)
        {
            _statisticsService = statisticsServer;
        }

        [HttpPost]
        public ActionResult RegisterAction([FromBody]AddActionViewModel action)
        {
            var cookieId = HttpContext.Request.Cookies["Id"];
            if(Guid.TryParse(cookieId, out Guid clientId))
            {
                if(action.ActionType == Misc.Common.Enums.ClientActionType.PageNavigation)
                {

                }
                _statisticsService.Add(action, clientId);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("Actions")]
        public ActionResult<List<ActionViewModel>> GetAllRegisteredActions()
        {
            var clientActions = _statisticsService.GetAll();
            return clientActions;
        }

        [HttpGet("ActionsPage/{page?}")]
        public ActionResult<PagedResult<ActionViewModel>> GetClientActionsPage(int? page)
        {
            var pageSize = 5;
            var clientActions = _statisticsService.GetClientActionsPage(page ?? 1, pageSize);
            return clientActions;
        }

        [HttpGet("PageViews")]
        public ActionResult<List<PageViewCountViewModel>> GetPageViewStatistics()
        {
            var pageViews = _statisticsService.GetPageViewStatistics();
            return pageViews;
        }

        [HttpGet("Clicks")]
        public ActionResult<List<ClickStatisticsViewModel>> GetClickStatistics()
        {
            var clickStatistics = _statisticsService.GetClickStatistics();
            return clickStatistics;
        }

        [HttpGet("Realtime")]
        public ActionResult<RealtimeStatisticsViewModel> GetRealtimeStatistics()
        {
            return new RealtimeStatisticsViewModel()
            {
                OnlineClients = TrackingMiddleware.OnlineClients.Select(p => p.Value).ToList()
            };
        }

        [HttpGet("DeviceUsage")]
        public ActionResult<List<DeviceUsageStatisticsViewModel>> GetDeviceUsageStatistics()
        {
            var statistics = _statisticsService.GetDeviceUsageStatistics();
            return statistics;
        }

        [HttpGet("Total")]
        public ActionResult<TotalStatisticsViewModel> GetTotalStatistics()
        {
            var totalStatistics = _statisticsService.GetTotalStatistics();
            return totalStatistics;
        }

        [HttpGet("Clients")]
        public ActionResult<List<ClientViewModel>> GetAllClients()
        {
            var clients = _statisticsService.GetAllClients();
            return clients;
        }

        [HttpGet("ClientsPage/{page?}")]
        public ActionResult<PagedResult<ClientViewModel>> GetClientsPage(int? page)
        {
            var pageSize = 5;
            var clients = _statisticsService.GetClientsPage(page ?? 1, pageSize);
            return clients;
        }

        [HttpGet("DailyViews")]
        public ActionResult<List<DailyViewStatisticsViewModel>> GetDailyViewStatistics()
        {
            var views = _statisticsService.GetDailyViewStatistics();
            return views;
        }
    }
}