using System;

namespace WebAnalytics.Presentation.ViewModels
{
    public class ClientViewModel
    {
        public Guid ClientId { get; set; }
        public string Ip { get; set; }
        public string Device { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public string Platform { get; set; }
        public string PlatformVersion { get; set; }
    }
}
