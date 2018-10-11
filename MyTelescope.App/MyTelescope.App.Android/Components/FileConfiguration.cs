using MyTelescope.App.Droid.Components;

using Xamarin.Forms;

[assembly: Dependency(typeof(FileConfiguration))]

namespace MyTelescope.App.Droid.Components
{
    using Android.App;
    using Android.Content;

    using System.IO;
    using System.Threading.Tasks;

    using Utilities.Interfaces;

    public class FileConfiguration : IFileConfiguration
    {
        private readonly Context _context = Application.Context;

        public async Task<string> ReadAsString(string fileName)
        {
            using (var asset = _context.Assets.Open(fileName))
            {
                using (var streamReader = new StreamReader(asset))
                {
                    return await streamReader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
        }
    }
}