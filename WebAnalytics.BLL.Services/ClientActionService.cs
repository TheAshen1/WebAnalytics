using System.Collections.Generic;
using WebAnalytics.BLL.Mapper;
using WebAnalytics.DAL.Repositories.Interfaces;
using WebAnalytics.Services.Interfaces;
using WebAnalytics.Presentation.ViewModels;

namespace WebAnalytics.Services
{
    public class ClientActionService : IClientActionService
    {
        private readonly IClientActionRepository _clientActionRepository;

        public ClientActionService(IClientActionRepository clientActionRepository)
        {
            _clientActionRepository = clientActionRepository;
        }

        public void Add(AddClientActionViewModel clientActionViewModel)
        {
            var entity = Mapper.Map(clientActionViewModel);
            _clientActionRepository.Add(entity);
        }

        public List<ClientActionViewModel> GetAll()
        {
            var entities = _clientActionRepository.GetAll();
            return Mapper.Map(entities);
        }
    }
}
