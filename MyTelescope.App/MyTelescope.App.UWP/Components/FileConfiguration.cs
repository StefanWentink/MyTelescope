using MyTelescope.App.UWP.Components;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileConfiguration))]

namespace MyTelescope.App.UWP.Components
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Utilities.Helpers;
    using Utilities.Interfaces;

    public class FileConfiguration : IFileConfiguration
    {
        public async Task<string> ReadAsString(string fileName)
        {
            var bytes = await ReadAsBytes(fileName).ConfigureAwait(false);
            bytes = bytes.CleanByteOrderMark();
            return Encoding.UTF8.GetString(bytes);
        }

        private static async Task<byte[]> ReadAsBytes(string fileName)
        {
            var folderStructure = "Assets";
            var uri = new Uri($"ms-appx:///{folderStructure}/{fileName}", UriKind.Absolute);

            try
            {
                var fileTask = StorageFile.GetFileFromApplicationUriAsync(uri);
                var file = await fileTask;
                var buffer = await FileIO.ReadBufferAsync(file);

                using (var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(buffer))
                {
                    var bytes = new byte[buffer.Length];
                    dataReader.ReadBytes(bytes);
                    return bytes;
                }
            }
            catch (FileNotFoundException exception)
            {
                LogHelper.LogError(exception);
                return new byte[0];
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                return new byte[0];
            }
        }
    }
}