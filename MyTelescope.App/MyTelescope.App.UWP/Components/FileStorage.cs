using MyTelescope.App.UWP.Components;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileStorage))]

namespace MyTelescope.App.UWP.Components
{
    using System;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Utilities.Interfaces;

    // https://msdn.microsoft.com/library/windows/apps/xaml/hh758325.aspx
    public class FileStorage : IFileStorage
    {
        public async Task WriteAsString(string fileName, string text)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, text);
        }

        public async Task<string> ReadAsString(string fileName)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.GetFileAsync(fileName);
            return await FileIO.ReadTextAsync(file);
        }
    }
}