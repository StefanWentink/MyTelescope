namespace MyTelescope.App.ViewModels.Models
{
    public abstract class SelectListModel<TValue>
    {
        protected SelectListModel(TValue value, string description)
        {
            Description = description;
            Value = value;
        }

        public string Description { get; set; }

        public TValue Value { get; set; }
    }
}