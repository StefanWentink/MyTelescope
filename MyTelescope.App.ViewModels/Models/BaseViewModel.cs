namespace MyTelescope.App.ViewModels.Models
{
    using Interfaces;

    public abstract class BaseViewModel : IBaseViewModel
    {
        public bool Selected { get; set; }
    }
}
