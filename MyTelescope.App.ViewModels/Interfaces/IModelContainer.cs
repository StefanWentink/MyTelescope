namespace MyTelescope.App.ViewModels.Interfaces
{
    using SWE.Model.Interfaces;

    public interface IModelContainer<TModel>
        where TModel : class, IKey, new()
    {
        void SetModel(TModel model);

        TModel Model { get; }
    }
}