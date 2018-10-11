namespace MyTelescope.App.Utilities.EventArgs
{
    using System;

    public class ModelFetchedEventArgs<TModel> : EventArgs
        where TModel : class
    {
        public TModel Model { get; }

        public ModelFetchedEventArgs(TModel model)
        {
            Model = model;
        }
    }
}