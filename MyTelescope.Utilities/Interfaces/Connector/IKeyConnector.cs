namespace MyTelescope.Utilities.Interfaces.Connector
{
    using SWE.Model.Interfaces;
    using System;

    public interface IKeyConnector<TModel, in TKey> : IConnector<TModel>
            where TModel : class, IKey<TKey>
            where TKey : IEquatable<TKey>
    {
        TModel ReadBy(TKey key);
    }
}