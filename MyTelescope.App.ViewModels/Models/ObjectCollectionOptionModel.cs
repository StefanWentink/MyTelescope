namespace MyTelescope.App.ViewModels.Models
{
    using Utilities.Enums;

    public class ObjectCollectionOptionModel : SelectListModel<ObjectCollectionOption>
    {
        public ObjectCollectionOptionModel(ObjectCollectionOption value, string description) 
            : base(value, description)
        {
        }
    }
}