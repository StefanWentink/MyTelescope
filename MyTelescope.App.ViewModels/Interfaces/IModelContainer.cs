namespace MyTelescope.App.ViewModels.Interfaces
{
    using MyTelescope.Utilities.Interfaces;

    public interface IModelContainer<TModel>
        where TModel : class, IKeyModel, new()
    {
        void SetModel(TModel model);

        TModel Model { get; }
    }
}