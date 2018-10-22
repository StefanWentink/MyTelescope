namespace MyTelescope.App.Utilities.EventArgs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CollectionFetchedEventArgs<TModel> : EventArgs
        where TModel : class
    {
        public List<TModel> Models { get; }

        public int Count => Models?.Count ?? 0;

        public bool EndOfList { get; }

        public CollectionFetchedEventArgs(IEnumerable<TModel> models, bool endOfList)
        {
            Models = models.ToList();
            EndOfList = endOfList;
        }

        public void InsertAt(int index, TModel item)
        {
            Models.Insert(index, item);
        }
    }
}