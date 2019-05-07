using System.Collections.Generic;
using WebAnalytics.BLL.Mapper;
using WebAnalytics.DAL.Repositories.Interfaces;
using WebAnalytics.Services.Interfaces;
using WebAnalytics.Presentation.ViewModels;
using System.Linq;
using WebAnalytics.Misc.Common.Extensions;

namespace WebAnalytics.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IClientActionRepository _clientActionRepository;
        private readonly IUniqueUsersCounterRepository _uniqueUsersCounterRepository;

        public StatisticsService(IClientActionRepository clientActionRepository, IUniqueUsersCounterRepository uniqueUsersCounterRepository)
        {
            _clientActionRepository = clientActionRepository;
            _uniqueUsersCounterRepository = uniqueUsersCounterRepository;
        }

        public void Add(AddClientActionViewModel clientActionViewModel, string ip)
        {
            var entity = Mapper.Map(clientActionViewModel, ip);
            _clientActionRepository.Add(entity);
        }

        public List<ClientActionViewModel> GetAll()
        {
            var entities = _clientActionRepository.GetAll();
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
            var deviceUsage = _clientActionRepository.GetAll()
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
                TotalUniqueUsersCount = _uniqueUsersCounterRepository.Get(),
                TotalPageViewsCount = _clientActionRepository.GetPageNavigationsCount(),
                TotalClicksCount = _clientActionRepository.GetClicksCount(),
            };
        }
    }
}
