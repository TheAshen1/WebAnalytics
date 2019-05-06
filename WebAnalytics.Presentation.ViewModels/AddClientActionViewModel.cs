using WebAnalytics.Misc.Common.Enums;

namespace WebAnalytics.Presentation.ViewModels
{
    public class AddClientActionViewModel
    {
        public ClientActionType ActionType { get; set; }
        public string Url { get; set; }
        public string FromUrl { get; set; }
        public string Description { get; set; }
        public string Platform { get; set; }
        public string PlatformVersion { get; set; }
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public string OSArchitecture { get; set; }
        public string Product { get; set; }
    }
}
