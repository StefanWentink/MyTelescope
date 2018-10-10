namespace MyTelescope.App.DataLayer.Interfaces
{
    using System.Collections.Generic;
    using MyTelescope.Utilities.Models.Filter;

    public interface IDataTransponder
    {
        IEnumerable<TModel> Read<TModel>(FilterModel filter)
            where TModel : class;
    }
}