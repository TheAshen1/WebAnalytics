using System;

namespace WebAnalytics.DAL.Entities
{
    public class UniqueUser
    {
        public Guid Id { get; set; }
        public string Platform { get; set; }
        public string PlatformVersion { get; set; }
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public string OSArchitecture { get; set; }
        public string Device { get; set; }
    }
}
