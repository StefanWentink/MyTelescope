namespace MyTelescope.App.Extensions
{
    using Localisation.Resources.MyTelescope;
    using MyTelescope.App.Localisation.Interfaces;
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Resources;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    // You exclude the 'Extension' suffix when using in Xaml markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        private CultureInfo CultureInfo { get; }

        private static string ResourceFullName { get; } = typeof(TextResource).FullName;

        private static Lazy<ResourceManager> _resourceManager;

        private static Lazy<ResourceManager> ResourceManager =>
            _resourceManager ?? (_resourceManager = LoadLazyResourceManager());

        private static Lazy<ResourceManager> LoadLazyResourceManager()
        {
            var assembly = typeof(TranslateExtension).GetTypeInfo().Assembly;
            var result = new ResourceManager(ResourceFullName, assembly);
            return new Lazy<ResourceManager>(() => result);
        }

        public TranslateExtension()
        {
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                CultureInfo = DependencyService.Get<ILocalise>().GetCurrentCultureInfo();
            }
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return string.Empty;
            }

            var translation = ResourceManager.Value.GetString(Text, CultureInfo);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    $"Key '{Text}' was not found in resources '{ResourceFullName}' for culture '{CultureInfo?.Name}'.",
                    nameof(serviceProvider));
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }

            return translation;
        }
    }
}