namespace MyTelescope.App.ViewModels.Interfaces
{
    using SWE.Model.Interfaces;

    using System;

    public interface IBaseKeyViewModel<TModel> : IBaseViewModel
        where TModel : class, IKey, new()
    {
        Guid Id { get; } //set; }

        TModel Model { get; set; }
    }
}