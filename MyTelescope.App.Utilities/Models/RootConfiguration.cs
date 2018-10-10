namespace MyTelescope.App.Utilities.Models
{
    using Interfaces;

    public class RootConfiguration : IRootConfiguration
    {
        public RootConfiguration()
        {
            Infrastructure = new InfrastructureConfiguration();
            Platform = new PlatformConfiguration();
        }

        public IInfrastructureConfiguration Infrastructure { get; set; }

        public IPlatformConfiguration Platform { get; set; }
    }
}
