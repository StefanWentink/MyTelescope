namespace MyTelescope.App.Utilities.Models
{
    using Interfaces;

    public class PlatformConfiguration : IPlatformConfiguration
    {
        public string HockeyAppId { get; set; }
    }
}