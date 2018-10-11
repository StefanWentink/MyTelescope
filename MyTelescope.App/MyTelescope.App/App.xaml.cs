namespace MyTelescope.App
{
    using Helpers;
    using Models;
    using SolarSystem.Helpers.Seeder;
    using System.Threading.Tasks;
    using Utilities.Helpers;
    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            StartupHelper.Initialize();

            DataConnectionHelper.InitDataConnection();

            InitializeComponent();

            Task.Run(GeoLocationHelper.SetGeoLocation).ConfigureAwait(false);

            var mainpage = new MainPageModel(CelestialObjectSeedHelper.GetSun());

            mainpage.SetModel(null);

            MainPage = mainpage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}