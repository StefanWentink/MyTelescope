namespace MyTelescope.App.Interfaces
{
    using System.Threading.Tasks;
    using FreshMvvm;
    using Xamarin.Forms;

    public interface IFreshNavigationService
    {
        Task PopToRoot(bool animate = true);

        Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true);

        Task PopPage(bool modal = false, bool animate = true);
    }
}
