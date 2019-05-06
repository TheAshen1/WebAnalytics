using System;

namespace WebAnalytics.Presentation.ViewModels
{
    public class OnlineUserViewModel
    {
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
