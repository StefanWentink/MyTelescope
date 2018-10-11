namespace MyTelescope.App.DataLayer.Interfaces
{
    public interface ICrudDataExchanger<in TRequestModel> : IDataExchanger<TRequestModel>
        where TRequestModel : IRequestModel
    {
        string CreateAction { get; }

        string ReadAction { get; }

        string UpdateAction { get; }

        string DeleteAction { get; }
    }
}