using System.Collections.Generic;
using WebAnalytics.DAL.Entities;
using WebAnalytics.Misc.Common.Extensions;

namespace WebAnalytics.DAL.Repositories.Interfaces
{
    public interface IClientRepository
    {
        void Add(Client uniqueUser);
        List<Client> GetAll();
        int GetCount();
        PagedResult<ClientAction> GetPage(int page, int pageSize);
    }
}