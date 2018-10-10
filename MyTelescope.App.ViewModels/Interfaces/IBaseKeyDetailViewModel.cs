namespace MyTelescope.App.ViewModels.Interfaces
{
    using System.Collections.Generic;
    using MyTelescope.Utilities.Interfaces;

    public interface IBaseKeyDetailViewModel<TModel> : IBaseKeyViewModel<TModel>
        where TModel : class, IKeyModel, new()
    {
        List<IDetailViewModel> GetDetails();
    }
}
