namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using MyTelescope.Utilities.Interfaces;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using ViewModels.Interfaces;

    public abstract class HttpDataLoader<TViewModel, TModel> : 
        BaseDataLoader<TViewModel, TModel>, 
        IHttpDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKeyModel, new()
    {
        protected IConnector<TModel> Connector { get; set; }
        
        protected HttpDataLoader(IConnector<TModel> connector)
        {
            Connector = connector;
        }

        protected override async Task<List<TViewModel>> GetTask(TModel model, FilterModel filter)
        {
            var collection = await Connector.ReadAsync(filter).ConfigureAwait(false);
            return collection.Select(x => new TViewModel { Model = x }).ToList();
        }

        ~HttpDataLoader()
        {
            if (Connector == null)
            {
                Connector = null;
            }
        }
    }
}
