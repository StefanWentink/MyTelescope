namespace MyTelescope.Api.DataLayer.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface ISingletonFactory<TModel>
        where TModel : class, new()
    {
        List<TModel> List { get; }
    }
}
