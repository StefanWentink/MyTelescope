namespace MyTelescope.App.Test.Base
{
    using App.Utilities.Interfaces;
    using System.Threading.Tasks;

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