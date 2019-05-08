using System;
using System.Collections.Generic;
using WebAnalytics.Misc.Common.Extensions;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.Services.Interfaces
{
    public interface IStatisticsService
    {
        void Add(AddActionViewModel clientActionViewModel, Guid clientId);
        List<ActionViewModel> GetAll();
        List<PageViewCountViewModel> GetPageViewStatistics();
        List<ClickStatisticsViewModel> GetClickStatistics();
        PagedResult<ActionViewModel> GetClientActionsPage(int page, int pageSize);
        List<DeviceUsageStatisticsViewModel> GetDeviceUsageStatistics();
        TotalStatisticsViewModel GetTotalStatistics();
        List<ClientViewModel> GetAllClients();
        List<DailyViewStatisticsViewModel> GetDailyViewStatistics();
        PagedResult<ClientViewModel> GetClientsPage(int page, int pageSize);
        List<AverageTimeOnPageViewModel> GetAverageTimeOnPageStatistics();
        //void CalculateAndSaveTimeOnPage(AddActionViewModel clientActionViewModel, Guid clientId);
    }
}