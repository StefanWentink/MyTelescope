namespace MyTelescope.App.Test.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataLayer.Models.DataLoader;
    using MyTelescope.Utilities.Interfaces;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using ViewModels.Interfaces;

    public abstract class DummyBatchDataLoader<TViewModel, TModel> : 
        BatchDataLoader<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKeyModel, new()
    {
        protected IConnector<TModel> Connector { get; set; }

        protected DummyBatchDataLoader(IConnector<TModel> connector)
            : this(connector, new List<TViewModel>())
        {
        }

        protected DummyBatchDataLoader(IConnector<TModel> connector, List<TViewModel> list)
        : base (list)
        {
            Connector = connector;
        }
        
        protected override async Task<List<TModel>> ReadAsync(FilterModel filter)
        {
            return await Connector.ReadAsync(filter);
        }

        protected abstract TModel GetModel(int skip);

        ~DummyBatchDataLoader()
        {
            if (Connector == null)
            {
                Connector = null;
            }
        }
    }
}