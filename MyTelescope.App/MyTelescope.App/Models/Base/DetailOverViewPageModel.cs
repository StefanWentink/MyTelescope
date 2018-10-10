namespace MyTelescope.App.Models.Base
{
    using System.Threading.Tasks;
    using DataLayer.Interfaces;
    using MyTelescope.Utilities.Interfaces;
    using ViewModels.Interfaces;

    public abstract class DetailOverViewPageModel<TViewModel, TModel> : 
        CollectionPageModel<TViewModel, TModel>
        where TViewModel : class, IBaseViewModel
        where TModel : class, IKeyModel, new()
    {
        protected DetailOverViewPageModel(TModel model)
            : base(model)
        {
        }

        protected DetailOverViewPageModel(IDataLoader<TViewModel, TModel> dataLoader)
            : base(dataLoader)
        {
        }

        protected override async Task ItemSelected(TViewModel selectedItem)
        {
            return;
        }

        ~DetailOverViewPageModel()
        {
            Model = null;
        }
    }
}