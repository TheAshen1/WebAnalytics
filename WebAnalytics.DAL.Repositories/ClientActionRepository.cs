using System.Collections.Generic;
using System.Linq;
using WebAnalytics.DAL.Context;
using WebAnalytics.DAL.Entities;
using WebAnalytics.DAL.Repositories.Interfaces;

namespace WebAnalytics.DAL.Repositories
{
    public class ClientActionRepository : IClientActionRepository
    {
        private readonly WebStatisticsContext _context;

        public ClientActionRepository(WebStatisticsContext context)
        {
            _context = context;
        }

        public void Add(ClientAction clientAction)
        {
            _context.ClientActions.Add(clientAction);
            _context.SaveChanges();
        }

        public List<ClientAction> GetAll()
        {
            return _context.ClientActions.ToList();
        }
    }
}
