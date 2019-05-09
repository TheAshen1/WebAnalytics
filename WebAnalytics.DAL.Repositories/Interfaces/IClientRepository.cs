using System.Collections.Generic;
using WebAnalytics.DAL.Entities;
using WebAnalytics.Misc.Common.Extensions;

namespace WebAnalytics.DAL.Repositories.Interfaces
{
    public interface IClientRepository
    {
        void Add(Client uniqueUser);
        List<Client> GetAll();
        Client Get(System.Guid clientId);
        int GetCount();
        PagedResult<Action> GetPage(int page, int pageSize);
    }
}