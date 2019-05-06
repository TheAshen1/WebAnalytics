﻿using System;
using WebAnalytics.Misc.Common.Enums;

namespace WebAnalytics.DAL.Entities
{
    public class ClientAction
    {
        public Guid Id { get; set; }
        public string Ip { get; set; }
        public ClientActionType ActionType { get; set; }
        public string Url { get; set; }
        public string FromUrl { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Platform { get; set; }
        public string PlatformVersion { get; set; }
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public string OSArchitecture { get; set; }
        public string Device { get; set; }
    }
}
