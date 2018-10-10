namespace MyTelescope.App.Pages.Content
{
    using Helpers;
    using MyTelescope.Utilities.Helpers;
    using SkiaSharp.Views.Forms;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoonComparePage : ContentPage
    {
        public MoonComparePage()
        {
            InitializeComponent();
        }

        public void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            args.OnPaintSurface(ModelHelper.GetName(GetType().Name));
        }
    }
}