namespace MyTelescope.Utilities.Helpers
{
    using System;

    public static class DirectoryHelper
    {
        public static string GetCurrentMainDirectory()
        {
            var result = System.IO.Directory.GetCurrentDirectory();

            return GetCurrentMainDirectory(result);
        }

        public static string GetCurrentMainDirectory(string directory)
        {
            var position = directory.IndexOf(@"\bin\", StringComparison.OrdinalIgnoreCase);

            if (position > 0)
            {
                return directory.Substring(0, position);
            }

            return directory;
        }
    }
}
