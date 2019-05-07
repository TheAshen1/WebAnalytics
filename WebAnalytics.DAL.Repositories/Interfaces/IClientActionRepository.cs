using System.Collections.Generic;
using WebAnalytics.DAL.Entities;
using WebAnalytics.Misc.Common.Extensions;

namespace WebAnalytics.DAL.Repositories.Interfaces
{
    public interface IClientActionRepository
    {
        void Add(ClientAction clientAction);
        List<ClientAction> GetAll();
        List<ClientAction> GetPageNavigations();
        int GetPageNavigationsCount();

        List<ClientAction> GetClicks();
        int GetClicksCount();

        PagedResult<ClientAction> GetPage(int page, int pageSize);
    }
}