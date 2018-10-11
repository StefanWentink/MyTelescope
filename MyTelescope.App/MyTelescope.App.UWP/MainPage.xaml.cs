namespace MyTelescope.App.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new MyTelescope.App.App());
        }
    }
}