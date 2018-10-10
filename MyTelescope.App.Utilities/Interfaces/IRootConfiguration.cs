namespace MyTelescope.App.Utilities.Interfaces
{
    public interface IRootConfiguration
    {
        IInfrastructureConfiguration Infrastructure { get; }

        IPlatformConfiguration Platform { get; }
    }
}