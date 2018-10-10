namespace MyTelescope.App.DataLayer.Models.DataLoader
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using MyTelescope.Utilities.Interfaces;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using Utilities.Helpers;
    using ViewModels.Interfaces;

    public abstract class HttpDataLoader<TViewModel, TModel> : 
        BatchDataLoader<TViewModel, TModel>, 
        IHttpDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKeyModel, new()
    {
        protected IConnector<TModel> Connector { get; set; }

        protected HttpDataLoader(IConnector<TModel> connector)
            : this(connector, new List<TViewModel>())
        {
        }

        protected HttpDataLoader(IConnector<TModel> connector, List<TViewModel> list)
        : base (list)
        {
            Connector = connector;
        }
        
        protected override async Task<List<TModel>> ReadAsync(FilterModel filter)
        {
            return await Connector.ReadAsync(filter);
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
