using System.Collections.Generic;
using System.Linq;
using WebAnalytics.DAL.Context;
using WebAnalytics.DAL.Entities;
using WebAnalytics.DAL.Repositories.Interfaces;
using WebAnalytics.Misc.Common.Extensions;

namespace WebAnalytics.DAL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly WebStatisticsContext _context;

        public ClientRepository(WebStatisticsContext context)
        {
            _context = context;
        }

        public void Add(Client uniqueUser)
        {
            _context.UniqueClients.Add(uniqueUser);
            _context.SaveChanges();
        }

        public List<Client> GetAll()
        {
            return _context.UniqueClients.ToList();
        }

        public int GetCount()
        {
            return _context.UniqueClients.Count();
        }

        public PagedResult<ClientAction> GetPage(int page, int pageSize)
        {
            return _context.ClientActions.GetPaged(page, pageSize);
        }
    }
}
