using System.Collections.Generic;
using WebAnalytics.BLL.Mapper;
using WebAnalytics.DAL.Repositories.Interfaces;
using WebAnalytics.Services.Interfaces;
using WebAnalytics.Presentation.ViewModels;
using System.Linq;

namespace WebAnalytics.Services
{
    public class ClientActionService : IClientActionService
    {
        private readonly IClientActionRepository _clientActionRepository;

        public ClientActionService(IClientActionRepository clientActionRepository)
        {
            _clientActionRepository = clientActionRepository;
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
                .Select(group => new ClickStatisticsViewModel() {
                    Description = group.Key,
                    Count = group.Count()
                }).ToList();
            return clickStatistics;
        }

        public List<PageViewCountViewModel> GetPageViewStatistics()
        {
            var pageViews = _clientActionRepository.GetPageNavigations()
                .GroupBy(a => a.Url)
                .Select(group => new PageViewCountViewModel() {
                    Url = group.Key,
                    Count = group.Count()
                }).ToList();
            return pageViews;
        }
    }
}
