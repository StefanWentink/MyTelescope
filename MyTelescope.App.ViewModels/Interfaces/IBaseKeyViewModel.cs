namespace MyTelescope.App.ViewModels.Interfaces
{
    using System;
    using MyTelescope.Utilities.Interfaces;

    public interface IBaseKeyViewModel<TModel> : IBaseViewModel
        where TModel : class, IKeyModel, new()
    {
        Guid Id { get; set; }

        TModel Model { get; set; }
    }
}
