namespace MyTelescope.App.Models.Base
{
    using Helpers;
    using MyTelescope.Data.Loader.Interfaces;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Utilities.Enums;
    using Utilities.EventArgs;
    using Utilities.Models;
    using ViewModels.Interfaces;

    public abstract class CanvasPageModel<TViewModel, TModel> : OverviewPageModel<TViewModel, TModel>
        where TViewModel : class, IBaseKeyViewModel<TModel>, new()
        where TModel : class, IKey, new()
    {
        private readonly object _shapesCollectionLock = new object();

        public abstract string CanvasViewKey { get; }

        public ObservableCollection<CelestialDrawModel> Shapes { get; set; } = new ObservableCollection<CelestialDrawModel>();

        protected override bool AddToCollection { get; set; }

        protected CanvasPageModel(IDataLoader<TViewModel, TModel> dataLoader)
            : base(dataLoader)
        {
            //AddToCollection = false;
        }

        protected override async Task ItemSelected(TViewModel selectedItem)
        {
            await Task.Delay(1).ConfigureAwait(false);
        }

        protected override void CollectionFetchedHandler(object sender, CollectionFetchedEventArgs<TViewModel> args)
        {
            if (args.Models == null || args.Models.Count == 0)
            {
                return;
            }

            if (Model != null && args.Models.All(x => x.Id != Model.Id) && !Collection.Any())
            {
                args.InsertAt(0, new TViewModel { Model = Model });
            }

            base.CollectionFetchedHandler(sender, args);
        }

        protected abstract void PrepareShapes();

        protected void SetShapes(ICollection<CelestialDrawModel> shapes)
        {
            SetShapes(shapes, ObjectCollectionLayout.Position);
        }

        protected void SetShapes(ICollection<CelestialDrawModel> shapes, ObjectCollectionLayout objectCollectionLayout)
        {
            CanvasSessionHelper.SetCanvasShapes(CanvasViewKey, shapes.ToList());
            CanvasSessionHelper.SetCanvasObjectCollectionLayout(CanvasViewKey, objectCollectionLayout);
            Shapes.SetOnApplicationThread(shapes, _shapesCollectionLock, RaisePropertyChanged, nameof(Shapes), null);
        }

        /// <summary>
        /// This method is called when the view is disappearing.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The e. </param>
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            CanvasSessionHelper.ClearShapes(CanvasViewKey);
        }

        /// <summary>
        /// This methods is called when the View is appearing
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> The e. </param>
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            PrepareShapes();
        }

        ~CanvasPageModel()
        {
            CanvasSessionHelper.ClearShapes(CanvasViewKey);
        }
    }
}