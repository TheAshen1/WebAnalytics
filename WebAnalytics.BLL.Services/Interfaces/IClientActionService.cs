using System.Collections.Generic;
using WebAnalytics.Misc.Common.Extensions;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.Services.Interfaces
{
    public interface IStatisticsService
    {
        void Add(AddClientActionViewModel clientActionViewModel, string ip);
        List<ClientActionViewModel> GetAll();
        List<PageViewCountViewModel> GetPageViewStatistics();
        List<ClickStatisticsViewModel> GetClickStatistics();
        PagedResult<ClientActionViewModel> GetPage(int page, int pageSize);
        List<DeviceUsageStatisticsViewModel> GetDeviceUsageStatistics();
        TotalStatisticsViewModel GetTotalStatistics();
    }
}