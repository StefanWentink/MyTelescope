namespace MyTelescope.App.Utilities.EventArgs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MyTelescope.Utilities.Interfaces;

    public class CollectionFetchedEventArgs<TModel> : EventArgs
        where TModel : class
    {
        public List<TModel> Models { get; }

        public CollectionFetchedEventArgs(IEnumerable<TModel> models)
        {
            Models = models.ToList();
        }

        public void InsertAt(int index, TModel item)
        {
            Models.Insert(index, item);
        }
    }
}
