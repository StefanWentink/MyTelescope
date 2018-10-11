using MyTelescope.App.Droid.Components;

using Xamarin.Forms;

[assembly: Dependency(typeof(FileStorage))]

namespace MyTelescope.App.Droid.Components
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Utilities.Interfaces;

    public class FileStorage : IFileStorage
    {
        public Task WriteAsString(string fileName, string text)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            return Task.Run(() => File.WriteAllText(filePath, text));
        }

        public async Task<string> ReadAsString(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            return await Task.Run(() => File.ReadAllText(filePath)).ConfigureAwait(false);
        }
    }
}