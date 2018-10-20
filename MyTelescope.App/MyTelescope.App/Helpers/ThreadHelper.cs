namespace MyTelescope.App.Helpers
{
    using MyTelescope.Core.Utilities.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Utilities.Helpers;
    using Xamarin.Forms;

    public static class ThreadHelper
    {
        public static void SetOnApplicationThread<TModel>(
            this ICollection<TModel> collection,
            IEnumerable<TModel> models,
            object writeLock,
            Action<string> propertyRaisedTask,
            string propertyName,
            Action followUpTask)
        {
            collection.PutOnApplicationThread(models, writeLock, propertyRaisedTask, propertyName, true, followUpTask);
        }

        public static void InsertOnApplicationThread<TModel>(
            this ObservableCollection<TModel> collection,
            TModel model,
            object writeLock,
            Action<string> propertyRaisedTask,
            string propertyName,
            int index,
            Action followUpTask)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    lock (writeLock)
                    {
                        collection.Insert(index, model);
                    }

                    propertyRaisedTask?.Invoke(propertyName);

                    followUpTask?.Invoke();
                }
                catch (Exception exception)
                {
                    LogHelper.LogError(exception);
                }
            });
        }

        public static void PutOnApplicationThread<TModel>(
            this ICollection<TModel> collection,
            IEnumerable<TModel> models,
            object writeLock,
            Action<string> propertyRaisedTask,
            string propertyName,
            bool clear,
            Action followUpTask)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    lock (writeLock)
                    {
                        if (clear)
                        {
                            collection.Clear();
                        }

                        foreach (var model in models)
                        {
                            collection.Add(model);
                        }
                    }

                    propertyRaisedTask?.Invoke(propertyName);

                    followUpTask?.Invoke();
                }
                catch (Exception exception)
                {
                    LogHelper.LogError(exception);
                    throw;
                }
            });
        }

        public static void RaiseOnApplicationThread(this Action<string> propertyRaisedTask, string propertyName)
        {
            Device.BeginInvokeOnMainThread(() => propertyRaisedTask.Invoke(propertyName));
        }
    }
}