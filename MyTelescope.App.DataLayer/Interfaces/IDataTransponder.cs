namespace MyTelescope.App.DataLayer.Interfaces
{
    using MyTelescope.Utilities.Models.Filter;
    using System.Collections.Generic;

    public interface IDataTransponder
    {
        IEnumerable<TModel> Read<TModel>(FilterModel filter)
            where TModel : class;
    }
}