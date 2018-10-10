namespace MyTelescope.App.Models
{
    using System.Threading.Tasks;
    using FreshMvvm;
    using SolarSystem.Models.CelestialObject;
    using ViewModels.Interfaces;
    using Xamarin.Forms;

    public class CustomNavigationService : NavigationPage, IFreshNavigationService
    {
        public CustomNavigationService(IModelContainer<CelestialObjectTypeModel> page)
            : base((Page)page)
        {
        }

        public CustomNavigationService(Page page) 
            : base(page)
        {
        }

        public async Task PopToRoot(bool animate = true)
        {
            await Navigation.PopToRootAsync(animate).ConfigureAwait(false);
        }

        public async Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            if (modal)
            {
                await Navigation.PushModalAsync(page, animate).ConfigureAwait(false);
            }
            else
            {
                await Navigation.PushAsync(page, animate).ConfigureAwait(false);
            }
        }

        public async Task PopPage(bool modal = false, bool animate = true)
        {
            if (modal)
            {
                await Navigation.PopModalAsync(animate).ConfigureAwait(false);
            }
            else
            {
                await Navigation.PopAsync(animate).ConfigureAwait(false);
            }
        }

        public Task<FreshBasePageModel> SwitchSelectedRootPageModel<TPage>() 
            where TPage : FreshBasePageModel
        {
            throw new System.NotImplementedException();
        }

        public void NotifyChildrenPageWasPopped()
        {
            //throw new System.NotImplementedException();
        }

        public string NavigationServiceName => GetType().Name;
    }
}
