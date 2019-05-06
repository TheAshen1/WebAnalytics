using System.Collections.Generic;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.Services.Interfaces
{
    public interface IClientActionService
    {
        void Add(AddClientActionViewModel clientActionViewModel, string ip);
        List<ClientActionViewModel> GetAll();
        List<PageViewCountViewModel> GetPageViewStatistics();
        List<ClickStatisticsViewModel> GetClickStatistics();
    }
}