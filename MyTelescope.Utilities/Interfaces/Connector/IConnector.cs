namespace MyTelescope.Utilities.Interfaces.Connector
{
    using Models.Filter;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IConnector<TModel>
        where TModel : class
    {
        List<TModel> Read(FilterModel filter);

        Task<List<TModel>> ReadAsync(FilterModel filter);

        Task<bool> CreateAsync(TModel model);

        Task<bool> CreateAsync(List<TModel> models);

        Task<bool> UpdateAsync(TModel model);

        Task<bool> UpdateAsync(List<TModel> models);

        Task<bool> DeleteAsync(TModel model);

        Task<bool> DeleteAsync(List<TModel> models);
    }
}