namespace MyTelescope.Utilities.Interfaces.Connector
{
    using Models.Sort;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IContextConnector<TModel> : IConnector<TModel>, IDisposable
        where TModel : class
    {
        IQueryable<TModel> Queryable { get; }

        Task<IQueryable<TModel>> QueryableAsync { get; }

        Task<List<TModel>> ReadAsync(Expression<Func<TModel, bool>> expression, SortModel sort);
    }
}