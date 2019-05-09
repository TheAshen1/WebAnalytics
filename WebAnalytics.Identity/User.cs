using System;

namespace WebAnalytics.Misc.Identity
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
