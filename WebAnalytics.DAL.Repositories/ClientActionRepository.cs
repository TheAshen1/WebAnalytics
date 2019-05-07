using System.Collections.Generic;
using System.Linq;
using WebAnalytics.DAL.Context;
using WebAnalytics.DAL.Entities;
using WebAnalytics.DAL.Repositories.Interfaces;
using WebAnalytics.Misc.Common.Enums;
using WebAnalytics.Misc.Common.Extensions;

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

        public List<ClientAction> GetClicks()
        {
            return _context.ClientActions
                .Where(a => a.ActionType == ClientActionType.Click)
                .ToList();
        }

        public int GetClicksCount()
        {
            return _context.ClientActions
                .Where(a => a.ActionType == ClientActionType.Click)
                .Count();
        }

        public PagedResult<ClientAction> GetPage(int page, int pageSize)
        {
            return _context.ClientActions.GetPaged(page, pageSize);
        }

        public List<ClientAction> GetPageNavigations()
        {
            return _context.ClientActions
                .Where(a => a.ActionType == ClientActionType.PageNavigation)
                .ToList();
        }

        public int GetPageNavigationsCount()
        {
            return _context.ClientActions
                .Where(a => a.ActionType == ClientActionType.PageNavigation)
                .Count();
        }
    }
}
