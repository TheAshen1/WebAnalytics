using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
            if (Guid.TryParse(cookieId, out Guid clientId))
            {
                _statisticsService.Add(action, clientId);
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Actions")]
        public ActionResult<List<ActionViewModel>> GetAllRegisteredActions()
        {
            var clientActions = _statisticsService.GetAll();
            return clientActions;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ActionsPage/{page?}")]
        public ActionResult<PagedResult<ActionViewModel>> GetClientActionsPage(int? page)
        {
            var pageSize = 5;
            var clientActions = _statisticsService.GetClientActionsPage(page ?? 1, pageSize);
            return clientActions;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("PageViews")]
        public ActionResult<List<PageViewCountViewModel>> GetPageViewStatistics()
        {
            var pageViews = _statisticsService.GetPageViewStatistics();
            return pageViews;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Clicks")]
        public ActionResult<List<ClickStatisticsViewModel>> GetClickStatistics()
        {
            var clickStatistics = _statisticsService.GetClickStatistics();
            return clickStatistics;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Realtime")]
        public ActionResult<RealtimeStatisticsViewModel> GetRealtimeStatistics()
        {
            return new RealtimeStatisticsViewModel()
            {
                OnlineClients = TrackingMiddleware.OnlineClients.Select(p => p.Value).ToList()
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("DeviceUsage")]
        public ActionResult<List<DeviceUsageStatisticsViewModel>> GetDeviceUsageStatistics()
        {
            var statistics = _statisticsService.GetDeviceUsageStatistics();
            return statistics;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Total")]
        public ActionResult<TotalStatisticsViewModel> GetTotalStatistics()
        {
            var totalStatistics = _statisticsService.GetTotalStatistics();
            return totalStatistics;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Clients")]
        public ActionResult<List<ClientViewModel>> GetAllClients()
        {
            var clients = _statisticsService.GetAllClients();
            return clients;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("ClientsPage/{page?}")]
        public ActionResult<PagedResult<ClientViewModel>> GetClientsPage(int? page)
        {
            var pageSize = 5;
            var clients = _statisticsService.GetClientsPage(page ?? 1, pageSize);
            return clients;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("DailyViews")]
        public ActionResult<List<DailyViewStatisticsViewModel>> GetDailyViewStatistics()
        {
            var views = _statisticsService.GetDailyViewStatistics();
            return views;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("AverageTimeOnPage")]
        public ActionResult<List<AverageTimeOnPageViewModel>> GetAverageTimeOnPageStatistics()
        {
            var statistics = _statisticsService.GetAverageTimeOnPageStatistics();
            return statistics;
        }
    }
}