﻿using MyTelescope.App.iOS.Components;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileStorage))]

namespace MyTelescope.App.iOS.Components
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Utilities.Interfaces;

    public class FileStorage : IFileStorage
    {
        public void WriteAsString(string fileName, string text)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            File.WriteAllText(filePath, text);
        }

        public async Task<string> ReadAsString(string fileName)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, fileName);
            return await Task.Run(() => File.ReadAllText(filePath));
        }
    }
}