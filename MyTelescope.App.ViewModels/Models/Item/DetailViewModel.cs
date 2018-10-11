namespace MyTelescope.App.ViewModels.Models.Item
{
    using Interfaces;

    public class DetailViewModel : BaseViewModel, IDetailViewModel
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public DetailViewModel(
            string code,
            string description)
        {
            Code = code;
            Description = description;
        }
    }
}