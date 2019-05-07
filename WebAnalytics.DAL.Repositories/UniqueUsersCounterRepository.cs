using System.Linq;
using WebAnalytics.DAL.Context;
using WebAnalytics.DAL.Repositories.Interfaces;

namespace WebAnalytics.DAL.Repositories
{
    public class UniqueUsersCounterRepository : IUniqueUsersCounterRepository
    {
        private readonly WebStatisticsContext _context;

        public UniqueUsersCounterRepository(WebStatisticsContext context)
        {
            _context = context;
        }

        public int Get()
        {
            return _context.UniqueUsersCounters.First().Counter;
        }

        public void Increment()
        {
            var entity = _context.UniqueUsersCounters.First();
            entity.Counter++;
            _context.SaveChanges();
        }
    }
}
