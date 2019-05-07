using System;
using System.Collections.Generic;
using WebAnalytics.DAL.Entities;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.BLL.Mapper
{
    public static class Mapper
    {
        public static List<ClientAction> Map(IList<ClientActionViewModel> viewModels)
        {
            var entities = new List<ClientAction>();
            foreach (var viewModel in viewModels)
            {
                entities.Add(Map(viewModel));
            }
            return entities;
        }

        public static ClientAction Map(ClientActionViewModel viewModel)
        {
            return new ClientAction()
            {
                ClientActionId = viewModel.ClientActionId,
                ClientId = viewModel.ClientId,
                ActionType = viewModel.ActionType,
                Url = viewModel.Url,
                FromUrl = viewModel.FromUrl,
                DateTime = viewModel.DateTime ?? DateTime.Now,
                Description = viewModel.Description,
            };
        }

        public static List<ClientActionViewModel> Map(IList<ClientAction> entities)
        {
            var viewModels = new List<ClientActionViewModel>();
            foreach (var entity in entities)
            {
                viewModels.Add(Map(entity));
            }
            return viewModels;
        }

        public static ClientActionViewModel Map(ClientAction entity)
        {
            return new ClientActionViewModel()
            {
                ClientActionId = entity.ClientActionId,
                ClientId = entity.ClientId,
                ActionType = entity.ActionType,
                Url = entity.Url,
                FromUrl = entity.FromUrl,
                DateTime = entity.DateTime,
                Description = entity.Description,
            };
        }

        public static List<ClientAction> Map(IList<AddClientActionViewModel> viewModels, Guid clientId)
        {
            var entities = new List<ClientAction>();
            foreach (var viewModel in viewModels)
            {
                entities.Add(Map(viewModel, clientId));
            }
            return entities;
        }

        public static ClientAction Map(AddClientActionViewModel viewModel, Guid clientId)
        {
            return new ClientAction()
            {
                ClientActionId = Guid.Empty,
                ClientId = clientId,
                ActionType = viewModel.ActionType,
                Url = viewModel.Url,
                FromUrl = viewModel.FromUrl,
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
