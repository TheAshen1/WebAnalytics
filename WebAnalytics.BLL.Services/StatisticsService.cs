using System.Collections.Generic;
using WebAnalytics.BLL.Mapper;
using WebAnalytics.DAL.Repositories.Interfaces;
using WebAnalytics.Services.Interfaces;
using WebAnalytics.Presentation.ViewModels;
using System.Linq;
using WebAnalytics.Misc.Common.Extensions;
using System;

namespace WebAnalytics.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IClientActionRepository _clientActionRepository;
        private readonly IClientRepository _clientRepository;

        public StatisticsService(IClientActionRepository clientActionRepository, IClientRepository clientRepository)
        {
            _clientActionRepository = clientActionRepository;
            _clientRepository = clientRepository;
        }

        public void Add(AddClientActionViewModel clientActionViewModel, Guid clientId)
        {
            var entity = Mapper.Map(clientActionViewModel, clientId);
            _clientActionRepository.Add(entity);
        }

        public List<ClientActionViewModel> GetAll()
        {
            var entities = _clientActionRepository.GetAll();
            return Mapper.Map(entities);
        }

        public List<ClientViewModel> GetAllClients()
        {
            var entities = _clientRepository.GetAll();
            return Mapper.Map(entities);
        }

        public List<ClickStatisticsViewModel> GetClickStatistics()
        {
            var clickStatistics = _clientActionRepository.GetClicks()
                .GroupBy(a => a.Description)
                .Select(group => new ClickStatisticsViewModel()
                {
                    Description = group.Key,
                    Count = group.Count()
                }).ToList();
            return clickStatistics;
        }

        public List<DeviceUsageStatisticsViewModel> GetDeviceUsageStatistics()
        {
            var deviceUsage = _clientRepository.GetAll()
                .GroupBy(a => a.Device)
                .Select(group => new DeviceUsageStatisticsViewModel()
                {
                    Device = group.Key,
                    Count = group.Count()
                }).ToList();
            return deviceUsage;
        }

        public PagedResult<ClientActionViewModel> GetPage(int page, int pageSize)
        {
            var entitiesPage = _clientActionRepository.GetPage(page, pageSize);
            var viewModelsPage = new PagedResult<ClientActionViewModel>()
            {
                CurrentPage = entitiesPage.CurrentPage,
                PageCount = entitiesPage.PageCount,
                PageSize = entitiesPage.PageSize,
                RowCount = entitiesPage.RowCount,
                Results = Mapper.Map(entitiesPage.Results)
            };
            return viewModelsPage;
        }

        public List<PageViewCountViewModel> GetPageViewStatistics()
        {
            var pageViews = _clientActionRepository.GetPageNavigations()
                .GroupBy(a => a.Url)
                .Select(group => new PageViewCountViewModel()
                {
                    Url = group.Key,
                    Count = group.Count()
                }).ToList();
            return pageViews;
        }

        public TotalStatisticsViewModel GetTotalStatistics()
        {
            return new TotalStatisticsViewModel()
            {
                TotalUniqueUsersCount = _clientRepository.GetCount(),
                TotalPageViewsCount = _clientActionRepository.GetPageNavigationsCount(),
                TotalClicksCount = _clientActionRepository.GetClicksCount(),
            };
        }
    }
}
