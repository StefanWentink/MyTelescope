namespace MyTelescope.App.Extensions
{
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require

            var source =
                Device.RuntimePlatform == Device.Android ?
                GetSource() :
                $"Images/{GetSource()}";

            var imageSource = ImageSource.FromResource(source);

            return imageSource;
        }

        private string GetSource()
        {
            return string.IsNullOrEmpty(Source) || Source.EndsWith(".png") ? Source : Source + ".png";
        }
    }
}