using System;

namespace WebAnalytics.DAL.Entities
{
    public class TimeOnPage
    {
        public Guid TimeOnPageId { get; set; }
        public string Page { get; set; }
        public int SecondsSpent { get; set; }
    }
}
