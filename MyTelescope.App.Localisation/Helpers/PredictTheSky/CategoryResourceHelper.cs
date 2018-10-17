namespace MyTelescope.App.Localisation.Helpers.PredictTheSky
{
    using Constants.PredictTheSky;
    using Enums.PredictTheSky;
    using global::MyTelescope.Utilities.Exceptions;
    using Resources.PredictTheSky;

    public static class CategoryResourceHelper
    {
        internal static string GetApiObjectName(this Category category)
        {
            switch (category)
            {
                case Category.Aurora:
                    return CategoryConstants.Aurora;

                case Category.Comet:
                    return CategoryConstants.Comet;

                case Category.IridiumFlare:
                    return CategoryConstants.IridiumFlare;

                case Category.LunarEclipse:
                    return CategoryConstants.LunarEclipse;

                case Category.MeteorShower:
                    return CategoryConstants.MeteorShower;

                case Category.MoonPhase:
                    return CategoryConstants.MoonPhase;

                case Category.Planet:
                    return CategoryConstants.Planet;

                case Category.Satellite:
                    return CategoryConstants.Satellite;

                default:
                    throw new EnumArgumentException<Category>((int)category);
            }
        }

        public static string GetResourceSingular(this Category category)
        {
            switch (category)
            {
                case Category.Aurora:
                    return CategoryResource.Aurora;

                case Category.Comet:
                    return CategoryResource.Comet;

                case Category.IridiumFlare:
                    return CategoryResource.IridiumFlare;

                case Category.LunarEclipse:
                    return CategoryResource.LunarEclipse;

                case Category.MeteorShower:
                    return CategoryResource.MeteorShower;

                case Category.MoonPhase:
                    return CategoryResource.MoonPhase;

                case Category.Planet:
                    return CategoryResource.Planet;

                case Category.Satellite:
                    return CategoryResource.Satellite;

                default:
                    throw new EnumArgumentException<Category>((int)category);
            }
        }

        public static string GetResourcePlural(this Category category)
        {
            switch (category)
            {
                case Category.Aurora:
                    return CategoryResource.Auroras;

                case Category.Comet:
                    return CategoryResource.Comets;

                case Category.IridiumFlare:
                    return CategoryResource.IridiumFlares;

                case Category.LunarEclipse:
                    return CategoryResource.LunarEclipses;

                case Category.MeteorShower:
                    return CategoryResource.MeteorShowers;

                case Category.MoonPhase:
                    return CategoryResource.MoonPhases;

                case Category.Planet:
                    return CategoryResource.Planets;

                case Category.Satellite:
                    return CategoryResource.Satellites;

                default:
                    throw new EnumArgumentException<Category>((int)category);
            }
        }
    }
}