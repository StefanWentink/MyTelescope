namespace MyTelescope.App.Helpers
{
    using System;
    using System.Reflection;
    using Xamarin.Forms;

    public static class ImageConvertHelper
    {
        public static ImageSource Convert(string value)
        {
            if (value == null)
            {
                return null;
            }
            
            var source =
                Device.RuntimePlatform == Device.Android ?
                    $"{GetSource(value)}" :
                    $"MyTelescope.App.{GetSource(value)}";

            var imageSource = ImageSource.FromResource(source, ImageAssembly);

            return imageSource;
        }

        private static Assembly ImageAssembly { get; } = typeof(ImageConvertHelper).GetTypeInfo().Assembly;

        public static object ConvertBack(object value)
        {
            throw new NotImplementedException($"{nameof(ImageConvertHelper)}.{nameof(ConvertBack)} is not implemented.");
        }

        private static string GetSource(string value)
        {
            var source = value.ToLowerInvariant();
            return string.IsNullOrEmpty(source) || source.EndsWith(".png") ? source : source + ".png";
        }
    }
}
