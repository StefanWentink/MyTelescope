namespace MyTelescope.Utilities.Interfaces.Connector
{
    using Models.Sort;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IContextConnector<TModel> : IConnector<TModel>
    where TModel : class
    {
        //List<TModel> Read(Expression<Func<TModel, bool>> expression, SortModel sort);

        Task<List<TModel>> ReadAsync(Expression<Func<TModel, bool>> expression, SortModel sort);
    }
}