namespace MyTelescope.App.ViewModels.Constants
{
    using Localisation.Resources.MyTelescope;
    using Models;
    using Utilities.Enums;

    public static class SelectListConstants
    {
        public static readonly ObjectCollectionOptionModel ObjectCollectionOptionAll = new ObjectCollectionOptionModel(ObjectCollectionOption.All, TextResource.ObjectCollectionOptionAll);
        public static readonly ObjectCollectionOptionModel ObjectCollectionOptionInner = new ObjectCollectionOptionModel(ObjectCollectionOption.Inner, TextResource.ObjectCollectionOptionInner);
        public static readonly ObjectCollectionOptionModel ObjectCollectionOptionOuter = new ObjectCollectionOptionModel(ObjectCollectionOption.Outer, TextResource.ObjectCollectionOptionOuter);

        public static readonly ObjectCollectionLayoutModel ObjectCollectionLayoutDistance = new ObjectCollectionLayoutModel(ObjectCollectionLayout.Distance, TextResource.LayoutOptionDistance);
        public static readonly ObjectCollectionLayoutModel ObjectCollectionLayoutPosition = new ObjectCollectionLayoutModel(ObjectCollectionLayout.Position, TextResource.LayoutOptionPosition);
    }
}