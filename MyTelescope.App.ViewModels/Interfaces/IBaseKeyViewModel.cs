namespace MyTelescope.App.ViewModels.Interfaces
{
    using MyTelescope.Utilities.Interfaces;
    using System;

    public interface IBaseKeyViewModel<TModel> : IBaseViewModel
        where TModel : class, IKeyModel, new()
    {
        Guid Id { get; set; }

        TModel Model { get; set; }
    }
}