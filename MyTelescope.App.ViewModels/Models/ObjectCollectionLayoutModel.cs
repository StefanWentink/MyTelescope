namespace MyTelescope.App.ViewModels.Models
{
    using Utilities.Enums;

    public class ObjectCollectionLayoutModel : SelectListModel<ObjectCollectionLayout>
    {
        public ObjectCollectionLayoutModel(ObjectCollectionLayout value, string description)
            : base(value, description)
        {
        }
    }
}