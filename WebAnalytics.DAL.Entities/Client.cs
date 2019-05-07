using System;
using System.Collections.Generic;

namespace WebAnalytics.DAL.Entities
{
    public class Client
    {
        public Guid ClientId { get; set; }
        public string Ip { get; set; }
        public string Device { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public string Platform { get; set; }
        public string PlatformVersion { get; set; }

        public List<ClientAction> Actions { get; set; }
    }
}
