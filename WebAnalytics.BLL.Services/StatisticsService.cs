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
        private readonly IActionRepository _actionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITimeOnPageRepository _timeOnPageRepository;

        public StatisticsService(IActionRepository clientActionRepository, IClientRepository clientRepository, ITimeOnPageRepository timeOnPageRepository)
        {
            _actionRepository = clientActionRepository;
            _clientRepository = clientRepository;
            _timeOnPageRepository = timeOnPageRepository;
        }

        public void Add(AddActionViewModel clientActionViewModel, Guid clientId)
        {
            ActionViewModel lastNavigation = null;
            if (clientActionViewModel.ActionType == Misc.Common.Enums.ClientActionType.PageNavigation)
            {
                lastNavigation = Mapper.Map(_actionRepository.GetLastClientPageNavigation(clientId));
            }
            var entity = Mapper.Map(clientActionViewModel, clientId);
            _actionRepository.Add(entity);
            var currentNavigation = Mapper.Map(entity);
            if (entity.ActionType == Misc.Common.Enums.ClientActionType.PageNavigation)
            {
                var spentSeconds = CalculateSecondsSpentOnPage(lastNavigation, currentNavigation);
                if(spentSeconds == 0)
                {
                    return;
                }
                _timeOnPageRepository.Add(new DAL.Entities.TimeOnPage() {
                    SecondsSpent = spentSeconds,
                    Page = lastNavigation.Url
                });
            }
        }

        public List<ActionViewModel> GetAll()
        {
            var entities = _actionRepository.GetAll();
            return Mapper.Map(entities);
        }

        public List<ClientViewModel> GetAllClients()
        {
            var entities = _clientRepository.GetAll();
            return Mapper.Map(entities);
        }

        public List<ClickStatisticsViewModel> GetClickStatistics()
        {
            var clickStatistics = _actionRepository
                .GetClicks()
                .GroupBy(a => a.Description)
                .Select(group => new ClickStatisticsViewModel()
                {
                    Description = group.Key,
                    Count = group.Count()
                }).ToList();
            return clickStatistics;
        }

        public List<DailyViewStatisticsViewModel> GetDailyViewStatistics()
        {
            var dailyViews = _actionRepository
                .GetPageNavigations()
                .GroupBy(a => new DateTime(a.DateTime.Year, a.DateTime.Month, a.DateTime.Day))
                .Select(group => new DailyViewStatisticsViewModel()
                {
                    DateTime = group.Key,
                    Count = group.Count(),
                }).ToList();
            return dailyViews;
        }

        public List<DeviceUsageStatisticsViewModel> GetDeviceUsageStatistics()
        {
            var deviceUsage = _clientRepository
                .GetAll()
                .GroupBy(a => a.Device)
                .Select(group => new DeviceUsageStatisticsViewModel()
                {
                    Device = group.Key,
                    Count = group.Count()
                }).ToList();
            return deviceUsage;
        }

        public PagedResult<ActionViewModel> GetClientActionsPage(int page, int pageSize)
        {
            var entitiesPage = _actionRepository.GetClientActionsPage(page, pageSize);
            var viewModelsPage = new PagedResult<ActionViewModel>()
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
            var pageViews = _actionRepository
                .GetPageNavigations()
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
                TotalPageViewsCount = _actionRepository.GetPageNavigationsCount(),
                TotalClicksCount = _actionRepository.GetClicksCount(),
            };
        }

        public PagedResult<ClientViewModel> GetClientsPage(int page, int pageSize)
        {
            var entitiesPage = _actionRepository.GetClientsPage(page, pageSize);
            var viewModelsPage = new PagedResult<ClientViewModel>()
            {
                CurrentPage = entitiesPage.CurrentPage,
                PageCount = entitiesPage.PageCount,
                PageSize = entitiesPage.PageSize,
                RowCount = entitiesPage.RowCount,
                Results = Mapper.Map(entitiesPage.Results)
            };
            return viewModelsPage;
        }

        public List<AverageTimeOnPageViewModel> GetAverageTimeOnPageStatistics()
        {
            var statistics = _timeOnPageRepository
                .GetAll()
                .GroupBy(t => t.Page)
                .Select(group => new AverageTimeOnPageViewModel()
                {
                    Url = group.Key,
                    AverageTimeSeconds = (double)group.Sum(t => t.SecondsSpent) / group.Count()
                }).ToList();
            return statistics;
        }

        private int CalculateSecondsSpentOnPage(ActionViewModel lastNavigation, ActionViewModel currentNavigation)
        {
            if (lastNavigation is null || lastNavigation.ActionType != Misc.Common.Enums.ClientActionType.PageNavigation)
            {
                return 0;
            }
            if (String.IsNullOrWhiteSpace(currentNavigation.FromUrl) || currentNavigation.FromUrl != lastNavigation.Url)
            {
                return 0;
            }
            var spentSeconds = (int)(currentNavigation.DateTime - lastNavigation.DateTime).TotalSeconds;
            return spentSeconds;
        }
    }
}
