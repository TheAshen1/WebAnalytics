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

        public void Add(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public List<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public int GetCount()
        {
            return _context.Clients.Count();
        }

        public PagedResult<Action> GetPage(int page, int pageSize)
        {
            return _context.Actions.GetPaged(page, pageSize);
        }
    }
}
