using System.Collections.Generic;
using WebAnalytics.DAL.Entities;

namespace WebAnalytics.DAL.Repositories.Interfaces
{
    public interface IClientActionRepository
    {
        void Add(ClientAction clientAction);
        List<ClientAction> GetAll();
        List<ClientAction> GetPageNavigations();
        List<ClientAction> GetClicks();
    }
}