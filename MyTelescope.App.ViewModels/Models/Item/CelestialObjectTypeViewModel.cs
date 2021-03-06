﻿namespace MyTelescope.App.ViewModels.Models.Item
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using SolarSystem.Enums;
    using SolarSystem.Extensions;
    using SolarSystem.Models.CelestialObject;

    public class CelestialObjectTypeViewModel : BaseKeyViewModel<CelestialObjectTypeModel>
    {
        [Obsolete("Only for page generation.")]
        public CelestialObjectTypeViewModel()
            : base()
        {
        }

        public CelestialObjectTypeViewModel(CelestialObjectTypeModel model)
            : base(model)
        {
        }

        [Required]
        public string Code
        {
            get => Model.Code;
            set => Model.Code = value;
        }

        [Required]
        public string Description => "TODO - Implement.";

        [Required]
        public CelestialObjectType CelestialObjectType => CelestialObjectTypeExtensions.ToEnum(Model.Code);
    }
}
