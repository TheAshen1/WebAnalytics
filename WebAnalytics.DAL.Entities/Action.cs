using System;
using WebAnalytics.Misc.Common.Enums;

namespace WebAnalytics.DAL.Entities
{
    public class Action
    {
        public Guid ActionId { get; set; }
        public ClientActionType ActionType { get; set; }
        public string Url { get; set; }
        public string FromUrl { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }

        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
