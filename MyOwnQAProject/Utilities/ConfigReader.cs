namespace Enterprise.Automation.Framework.Utilities
{
    public class ConfigReader
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UrlSettings Urls { get; set; } = new UrlSettings();
    }

    public class UrlSettings
    {
        public string Flipkart { get; set; } = string.Empty;
        public string TargetAppUrl { get; set; } = string.Empty;
        public string Automation {  get; set; } = string.Empty;
        public string PIM {  get; set; } = string.Empty;
        public string Frames { get; set; } = string.Empty;
    }
}