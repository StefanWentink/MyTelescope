namespace MyTelescope.App.ViewModels.Models.Item
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Drawing;
    using Interfaces;
    using MyTelescope.Utilities.Helpers;
    using SolarSystem.Models.CelestialObject;

    public class CelestialObjectViewModel : BaseKeyViewModel<CelestialObjectModel> , IDetailViewModel
    {
        public CelestialObjectViewModel()
        {
        }

        public CelestialObjectViewModel(CelestialObjectModel model)
            : base(model)
        {
        }

        [Required]
        public string Code
        {
            get => Model.Code;
            set => Model.Code = value;
        }

        [NotMapped]
        public string Description => Model.Code;

        [NotMapped]
        public string ImageName => Model.Code;

        [NotMapped]
        public string ImageUrl => Model.ImageUrl;

        public Color Color => ColorHelper.GetColorFromHex(Model.ColorCode);
    }
}
