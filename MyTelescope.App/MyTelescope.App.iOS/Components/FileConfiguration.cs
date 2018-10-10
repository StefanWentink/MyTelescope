using MyTelescope.App.iOS.Components;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileConfiguration))]

namespace MyTelescope.App.iOS.Components
{
    using System.IO;
    using System.Threading.Tasks;
    using Utilities.Helpers;
    using Utilities.Interfaces;

    public class FileConfiguration : IFileConfiguration
    {
        private Task<byte[]> ReadAsBytes(string fileName)
        {
            var data = File.ReadAllBytes(fileName);

            data = data.CleanByteOrderMark();

            return Task.FromResult(data);
        }

        public async Task<string> ReadAsString(string fileName)
        {
            var data = await ReadAsBytes(fileName);

            if (data == null)
            {
                return string.Empty;
            }

            return System.Text.Encoding.UTF8.GetString(data);
        }
    }
}