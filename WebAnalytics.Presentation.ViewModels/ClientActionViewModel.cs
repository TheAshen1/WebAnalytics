using System;
using WebAnalytics.Misc.Common.Enums;

namespace WebAnalytics.Presentation.ViewModels
{
    public class ClientActionViewModel
    {
        public Guid Id { get; set; }
        public ClientActionType ActionType { get; set; }
        public string Url { get; set; }
        public string FromUrl { get; set; }
        public DateTime? DateTime { get; set; }
        public string Description { get; set; }
    }
}
