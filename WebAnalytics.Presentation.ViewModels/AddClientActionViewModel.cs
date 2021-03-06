﻿using WebAnalytics.Misc.Common.Enums;

namespace WebAnalytics.Presentation.ViewModels
{
    public class AddActionViewModel
    {
        public ClientActionType ActionType { get; set; }
        public string Url { get; set; }
        public string FromUrl { get; set; }
        public string Description { get; set; }
    }
}
