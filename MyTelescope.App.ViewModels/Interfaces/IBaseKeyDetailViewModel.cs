namespace MyTelescope.App.ViewModels.Interfaces
{
    using SWE.Model.Interfaces;
    using System.Collections.Generic;

    public interface IBaseKeyDetailViewModel<TModel> : IBaseKeyViewModel<TModel>
        where TModel : class, IKey, new()
    {
        List<IDetailViewModel> GetDetails();
    }
}