using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Presentation.ViewModels;
using WebAnalytics.Services.Interfaces;
using WebAnalytics.UI.Middleware;

namespace WebAnalytics.UI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientActionsController : ControllerBase
    {
        private readonly IClientActionService _clientActionService;

        public ClientActionsController(IClientActionService clientActionService)
        {
            _clientActionService = clientActionService;
        }

        [HttpPost]
        public ActionResult RegisterAction([FromBody]AddClientActionViewModel action)
        {
            _clientActionService.Add(action, HttpContext.Connection.RemoteIpAddress.ToString());
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<ClientActionViewModel>> GetAllRegisteredActions()
        {
            var clientActions = _clientActionService.GetAll();
            return clientActions;
        }

        [HttpGet("PageViewStatistics")]
        public ActionResult<List<PageViewCountViewModel>> GetPageViewStatistics()
        {
            var pageViews = _clientActionService.GetPageViewStatistics();
            return pageViews;
        }

        [HttpGet("ClickStatistics")]
        public ActionResult<List<ClickStatisticsViewModel>> GetClickStatistics()
        {
            var clickStatistics = _clientActionService.GetClickStatistics();
            return clickStatistics;
        }

        [HttpGet("GetRealtimeStatistics")]
        public ActionResult<RealtimeStatisticsViewModel> GetRealtimeStatistics()
        {
            return new RealtimeStatisticsViewModel()
            {
                OnlineUsers = TrackingMiddleware.OnlineUsers.Select(p => p.Value).ToList()
            };
        }
    }
}