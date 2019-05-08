using System;
using System.Collections.Generic;
using WebAnalytics.DAL.Entities;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.BLL.Mapper
{
    public static class Mapper
    {
        public static List<DAL.Entities.Action> Map(IList<ActionViewModel> viewModels)
        {
            var entities = new List<DAL.Entities.Action>();
            foreach (var viewModel in viewModels)
            {
                entities.Add(Map(viewModel));
            }
            return entities;
        }

        public static DAL.Entities.Action Map(ActionViewModel viewModel)
        {
            return new DAL.Entities.Action()
            {
                ActionId = viewModel.ActionId,
                ClientId = viewModel.ClientId,
                ActionType = viewModel.ActionType,
                Url = viewModel.Url,
                FromUrl = viewModel.FromUrl,
                DateTime = viewModel.DateTime ?? DateTime.Now,
                Description = viewModel.Description,
            };
        }

        public static List<ActionViewModel> Map(IList<DAL.Entities.Action> entities)
        {
            var viewModels = new List<ActionViewModel>();
            foreach (var entity in entities)
            {
                viewModels.Add(Map(entity));
            }
            return viewModels;
        }

        public static ActionViewModel Map(DAL.Entities.Action entity)
        {
            return new ActionViewModel()
            {
                ActionId = entity.ActionId,
                ClientId = entity.ClientId,
                ActionType = entity.ActionType,
                Url = entity.Url,
                FromUrl = entity.FromUrl,
                DateTime = entity.DateTime,
                Description = entity.Description,
            };
        }

        public static List<DAL.Entities.Action> Map(IList<AddActionViewModel> viewModels, Guid clientId)
        {
            var entities = new List<DAL.Entities.Action>();
            foreach (var viewModel in viewModels)
            {
                entities.Add(Map(viewModel, clientId));
            }
            return entities;
        }

        public static DAL.Entities.Action Map(AddActionViewModel viewModel, Guid clientId)
        {
            return new DAL.Entities.Action()
            {
                ActionId = Guid.Empty,
                ClientId = clientId,
                ActionType = viewModel.ActionType,
                Url = viewModel.Url,
                FromUrl = String.IsNullOrWhiteSpace(viewModel.FromUrl) ? null : viewModel.FromUrl,
                DateTime = DateTime.Now,
                Description = viewModel.Description,
            };
        }

        public static List<ClientViewModel> Map(IList<Client> entities)
        {
            var viewModels = new List<ClientViewModel>();
            foreach (var entity in entities)
            {
                viewModels.Add(Map(entity));
            }
            return viewModels;
        }

        public static ClientViewModel Map(Client entity)
        {
            return new ClientViewModel()
            {
                ClientId = entity.ClientId,
                Ip = entity.Ip,
                Device = entity.Device,
                Browser = entity.Browser,
                BrowserVersion = entity.BrowserVersion,
                Platform = entity.Platform,
                PlatformVersion = entity.PlatformVersion
            };
        }
    }
}
