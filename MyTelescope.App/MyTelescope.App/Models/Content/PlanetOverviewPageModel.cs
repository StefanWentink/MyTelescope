namespace MyTelescope.App.Pages.Content
{
    using System;
    using System.Threading.Tasks;
    using DataLayer.Interfaces;
    using Models.Base;
    using SolarSystem.Models.CelestialObject;
    using Tab;
    using Utilities.Helpers;
    using ViewModels.Models.Item;
    using Xamarin.Forms;

    public class PlanetOverviewPageModel : OverviewPageModel<CelestialObjectViewModel, CelestialObjectModel>
    {
        protected override bool AddToCollection { get; set; } = false;

        public PlanetOverviewPageModel(IHttpDataLoader<CelestialObjectViewModel, CelestialObjectModel> dataLoader)
            : base(dataLoader)
        {
        }

        protected override async Task ItemSelected(CelestialObjectViewModel selectedItem)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var tabbedNavigation = new PlanetNavigationPageModel(selectedItem.Model);
                    await CoreMethods.PushNewNavigationServiceModal(tabbedNavigation).ConfigureAwait(false);
                }
                catch (FreshTinyIoC.TinyIoCResolutionException exception)
                {
                    LogHelper.LogError(exception);
                    throw;
                }
                catch (Exception exception)
                {
                    LogHelper.LogError(exception);
                    throw;
                }
            });
        }
    }
}