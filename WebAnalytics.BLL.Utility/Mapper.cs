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
                Ip = viewModel.Ip,
                ActionType = viewModel.ActionType,
                Url = viewModel.Url,
                FromUrl = viewModel.FromUrl,
                DateTime = viewModel.DateTime ?? DateTime.Now,
                Description = viewModel.Description,
                Platform = viewModel.Platform,
                PlatformVersion = viewModel.PlatformVersion,
                OS = viewModel.OS,
                OSVersion = viewModel.OSVersion,
                OSArchitecture = viewModel.OSArchitecture,
                Device = viewModel.Device
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
                Ip = entity.Ip,
                ActionType = entity.ActionType,
                Url = entity.Url,
                FromUrl = entity.FromUrl,
                DateTime = entity.DateTime,
                Description = entity.Description,
                Platform = entity.Platform,
                PlatformVersion = entity.PlatformVersion,
                OS = entity.OS,
                OSVersion = entity.OSVersion,
                OSArchitecture = entity.OSArchitecture,
                Device = entity.Device
            };
        }

        public static List<ClientAction> Map(List<AddClientActionViewModel> viewModels, string ip)
        {
            var entities = new List<ClientAction>();
            foreach (var viewModel in viewModels)
            {
                entities.Add(Map(viewModel, ip));
            }
            return entities;
        }

        public static ClientAction Map(AddClientActionViewModel viewModel, string ip)
        {
            return new ClientAction()
            {
                Id = Guid.Empty,
                Ip = ip,
                ActionType = viewModel.ActionType,
                Url = viewModel.Url,
                FromUrl = viewModel.FromUrl,
                DateTime = DateTime.Now,
                Description = viewModel.Description,
                Platform = viewModel.Platform,
                PlatformVersion = viewModel.PlatformVersion,
                OS = viewModel.OS,
                OSVersion = viewModel.OSVersion,
                OSArchitecture = viewModel.OSArchitecture,
                Device = viewModel.Device
            };
        }
    }
}
