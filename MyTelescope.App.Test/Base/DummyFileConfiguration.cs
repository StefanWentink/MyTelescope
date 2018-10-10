namespace MyTelescope.App.Test.Base
{
    using System.Threading.Tasks;
    using App.Utilities.Interfaces;

    internal class DummyFileConfiguration : IFileConfiguration
    {
        public Task<string> ReadAsString(string fileName)
        {
            switch (fileName)
            {
                case "platform.json":
                    return Task.Run(() => "{\"HockeyAppId\": \"6xx6xxxxxxxxxxxxxx77x21\"}");

                case "config.json":
                    return Task.Run(() => "{\"Infrastructure\": {\"MyTeleScopeApi\": \"http://localhost:51425/\"}}");

                default:
                    return Task.Run(() => string.Empty);
            }
        }
    }
}