using System.Collections.Generic;
using WebAnalytics.DAL.Entities;

namespace WebAnalytics.DAL.Repositories.Interfaces
{
    public interface ITimeOnPageRepository
    {
        void Add(TimeOnPage timeOnPage);
        List<TimeOnPage> GetAll();
    }
}