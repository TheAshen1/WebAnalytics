using System;

namespace WebAnalytics.Presentation.ViewModels
{
    public class OnlineClientViewModel
    {
        public Guid ClientId { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
