using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebAnalytics.Presentation.ViewModels;
using WebAnalytics.Services.Interfaces;

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
            _clientActionService.Add(action);
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<ClientActionViewModel>> GetAllRegisteredActions()
        {
            var clientActions = _clientActionService.GetAll();
            return clientActions;
        }
    }
}