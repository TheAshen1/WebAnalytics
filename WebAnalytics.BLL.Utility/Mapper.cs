using System;
using System.Collections.Generic;
using WebAnalytics.DAL.Entities;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.BLL.Mapper
{
    public static class Mapper
    {
        public static List<ClientAction> Map(List<ClientActionViewModel> viewModels)
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
                Id = viewModel.Id,
                ActionType = viewModel.ActionType,
                Url = viewModel.Url,
                FromUrl = viewModel.FromUrl,
                DateTime = viewModel.DateTime ?? DateTime.Now,
                Description = viewModel.Description
            };
        }

        public static List<ClientActionViewModel> Map(List<ClientAction> entities)
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
                Id = entity.Id,
                ActionType = entity.ActionType,
                Url = entity.Url,
                FromUrl = entity.FromUrl,
                DateTime = entity.DateTime,
                Description = entity.Description
            };
        }

        public static List<ClientAction> Map(List<AddClientActionViewModel> viewModels)
        {
            var entities = new List<ClientAction>();
            foreach (var viewModel in viewModels)
            {
                entities.Add(Map(viewModel));
            }
            return entities;
        }

        public static ClientAction Map(AddClientActionViewModel viewModel)
        {
            return new ClientAction()
            {
                Id = Guid.Empty,
                ActionType = viewModel.ActionType,
                Url = viewModel.Url,
                FromUrl = viewModel.FromUrl,
                DateTime = DateTime.Now,
                Description = viewModel.Description
            };
        }
    }
}
