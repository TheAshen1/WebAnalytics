using System.Collections.Generic;
using WebAnalytics.DAL.Entities;
using WebAnalytics.Misc.Common.Extensions;

namespace WebAnalytics.DAL.Repositories.Interfaces
{
    public interface IActionRepository
    {
        void Add(Action clientAction);
        List<Action> GetAll();
        List<Action> GetPageNavigations();
        int GetPageNavigationsCount();

        List<Action> GetClicks();
        int GetClicksCount();

        PagedResult<Action> GetClientActionsPage(int page, int pageSize);
        PagedResult<Client> GetClientsPage(int page, int pageSize);
        Action GetLastClientPageNavigation(System.Guid clientId);
    }
}