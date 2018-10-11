namespace MyTelescope.App.ViewModels.Interfaces
{
    using MyTelescope.Utilities.Interfaces;
    using System.Collections.Generic;

    public interface IBaseKeyDetailViewModel<TModel> : IBaseKeyViewModel<TModel>
        where TModel : class, IKeyModel, new()
    {
        List<IDetailViewModel> GetDetails();
    }
}