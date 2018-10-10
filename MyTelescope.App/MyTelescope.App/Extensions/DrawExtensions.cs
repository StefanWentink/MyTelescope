namespace MyTelescope.App.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using MyTelescope.Utilities.Helpers;
    using SolarSystem.Extensions;
    using Utilities.Models;
    using ViewModels.Models.Item;

    public static class DrawExtensions
    {
        public static Func<CelestialObjectViewModel, CelestialDrawModel> ToCelestialCompareDrawModelFunction
        {
            get
            {
                return x => new CelestialDrawModel(x.Id, x.Description, x.Model.GetVolumetricMeanRadius(), x.Color);
            }
        }

        public static CelestialDrawModel ToCelestialOrbitDrawModel(
            this CelestialObjectPositionViewModel celestialObjectPosition,
            CelestialObjectViewModel celestialObject)
        {
            return new CelestialDrawModel(
                celestialObject.Id,
                celestialObject.Description,
                //celestialObjectPosition?.Model.Location.GetOrbitRadius() ?? 
                DistanceHelper.MillionKilometersToAstronomicalUnits(celestialObject.Model.SemiMajorAxis),
                celestialObjectPosition?.Model.Location,
                celestialObject.Color,
                3,
                70);
        }

        public static CelestialDrawModel ToCelestialBodyDrawModel(
            this CelestialObjectPositionViewModel celestialObjectPosition,
            CelestialObjectViewModel celestialObject)
        {
            return new CelestialDrawModel(
                celestialObject.Id,
                celestialObject.Description,
                celestialObject.Model.GetVolumetricMeanRadius(),
                celestialObjectPosition?.Model.Location,
                celestialObject.Color,
                90);
        }
    }
}