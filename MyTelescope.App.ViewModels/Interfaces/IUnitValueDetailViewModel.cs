namespace MyTelescope.App.ViewModels.Interfaces
{
    using System.Collections.Generic;
    using Enums;

    public interface IUnitValueDetailViewModel
    {
        IDetailViewModel GetDetailView();

        string Title { get; set; }

        string StringValue { get; set; }

        string Unit { get; set; }

        List<DisplayType> DisplayTypes { get; set; }
    }
}
