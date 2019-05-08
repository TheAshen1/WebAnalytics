using System.Collections.Generic;
using System.Linq;
using WebAnalytics.DAL.Context;
using WebAnalytics.DAL.Entities;
using WebAnalytics.DAL.Repositories.Interfaces;
using WebAnalytics.Misc.Common.Enums;
using WebAnalytics.Misc.Common.Extensions;

namespace WebAnalytics.DAL.Repositories
{
    public class ActionRepository : IActionRepository
    {
        private readonly WebStatisticsContext _context;

        public ActionRepository(WebStatisticsContext context)
        {
            _context = context;
        }

        public void Add(Action action)
        {
            _context.Actions.Add(action);
            _context.SaveChanges();
        }

        public List<Action> GetAll()
        {
            return _context.Actions.ToList();
        }

        public List<Action> GetClicks()
        {
            return _context.Actions
                .Where(a => a.ActionType == ClientActionType.Click)
                .ToList();
        }

        public int GetClicksCount()
        {
            return _context.Actions
                .Where(a => a.ActionType == ClientActionType.Click)
                .Count();
        }

        public PagedResult<Action> GetClientActionsPage(int page, int pageSize)
        {
            return _context.Actions.GetPaged(page, pageSize);
        }

        public PagedResult<Client> GetClientsPage(int page, int pageSize)
        {
            return _context.Clients.GetPaged(page, pageSize);
        }

        public List<Action> GetPageNavigations()
        {
            return _context.Actions
                .Where(a => a.ActionType == ClientActionType.PageNavigation)
                .ToList();
        }

        public int GetPageNavigationsCount()
        {
            return _context.Actions
                .Where(a => a.ActionType == ClientActionType.PageNavigation)
                .Count();
        }

        public Action GetLastClientPageNavigation(System.Guid clientId)
        {
            return _context.Actions
                .Where(a => a.ClientId == clientId)
                .OrderByDescending(a => a.DateTime)
                .FirstOrDefault();
        }
    }
}
