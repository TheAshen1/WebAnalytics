using System.Collections.Generic;
using System.Linq;
using WebAnalytics.DAL.Context;
using WebAnalytics.DAL.Entities;
using WebAnalytics.DAL.Repositories.Interfaces;

namespace WebAnalytics.DAL.Repositories
{
    public class TimeOnPageRepository : ITimeOnPageRepository
    {
        private readonly WebStatisticsContext _context;

        public TimeOnPageRepository(WebStatisticsContext context)
        {
            _context = context;
        }

        public void Add(TimeOnPage timeOnPage)
        {
            _context.TimesOnPages.Add(timeOnPage);
            _context.SaveChanges();
        }

        public List<TimeOnPage> GetAll()
        {
            return _context.TimesOnPages.ToList();
        }
    }
}
