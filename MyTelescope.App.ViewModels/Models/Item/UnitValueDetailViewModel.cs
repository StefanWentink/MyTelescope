namespace MyTelescope.App.ViewModels.Models.Item
{
    using Enums;
    using Interfaces;
    using System.Collections.Generic;

    public class UnitValueDetailViewModel<TValue> : IUnitValueDetailViewModel
    {
        public IDetailViewModel GetDetailView()
        {
            return new DetailViewModel(Title, $"{StringValue} {Unit}");
        }

        public string Title { get; set; }

        public TValue Value { get; set; }

        public string StringValue { get; set; }

        public string Unit { get; set; }

        public List<DisplayType> DisplayTypes { get; set; }

        public UnitValueDetailViewModel(
            string title,
            TValue value,
            string unit,
            DisplayType displayType)
        : this(title, value, unit, new List<DisplayType> { displayType })
        {
        }

        public UnitValueDetailViewModel(
            string title,
            TValue value,
            string unit,
            List<DisplayType> displayTypes)
        {
            Title = title;
            Value = value;
            StringValue = value.ToString();
            Unit = unit;
            DisplayTypes = displayTypes;
        }
    }
}