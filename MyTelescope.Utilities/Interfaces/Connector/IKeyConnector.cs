namespace MyTelescope.Utilities.Interfaces.Connector
{
    public interface IKeyConnector<TModel, in TKey> : IConnector<TModel>
    where TModel : class, IKeyModel<TKey>
    {
        TModel ReadBy(TKey key);
    }
}