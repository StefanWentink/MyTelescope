﻿namespace MyTelescope.SolarSystem.Constants
{
    using System.Collections.Generic;
    using Enums;
    using Models.Keplerian;

    public static class KeplerianValueConstants
    {
        public static List<KeplerianModel> PlanetKeplerianModels { get; } =
            new List<KeplerianModel>
            {
                new KeplerianModel(
                    CelestialObject.Mercury,
                    new KeplerianValueModel(0.38709927, 0.20563593, 7.00497902, 252.25032350, 77.45779628, 48.33076593),
                    new KeplerianValueModel(0.00000037, 0.00001906, -0.00594749, 149472.67411175, 0.16047689, -0.12534081)),
                new KeplerianModel(
                    CelestialObject.Venus,
                    new KeplerianValueModel(0.72333566, 0.00677672, 3.39467605, 181.97909950, 131.60246718, 76.67984255),
                    new KeplerianValueModel(0.00000390, -0.00004107, -0.00078890, 58517.81538729, 0.00268329, -0.27769418)),
                new KeplerianModel(
                    CelestialObject.Earth,
                    new KeplerianValueModel(1.00000011, 0.01671022, 0.00005, 100.46435, 102.94719, -11.26064),
                    new KeplerianValueModel(0.00000562, -0.00004392, -0.01294668, 35999.37244981, 0.32327364, 0.0)),
                new KeplerianModel(
                    CelestialObject.Mars,
                    new KeplerianValueModel(1.52371034, 0.09339410, 1.84969142, -4.55343205, -23.94362959, 49.55953891),
                    new KeplerianValueModel(0.00001847, 0.00007882, -0.00813131, 19140.30268499, 0.44441088, -0.29257343)),
                new KeplerianModel(
                    CelestialObject.Jupiter,
                    new KeplerianValueModel(5.20288700, 0.04838624, 1.30439695, 34.39644051, 14.72847983, 100.47390909),
                    new KeplerianValueModel(-0.00011607, -0.00013253, -0.00183714, 3034.74612775, 0.21252668, 0.20469106)),
                new KeplerianModel(
                    CelestialObject.Saturn,
                    new KeplerianValueModel(9.53667594, 0.05386179, 2.48599187, 49.95424423, 92.59887831, 113.66242448),
                    new KeplerianValueModel(-0.00125060, -0.00050991, 0.00193609, 1222.49362201, -0.41897216, -0.28867794)),
                new KeplerianModel(
                    CelestialObject.Uranus,
                    new KeplerianValueModel(19.18916464, 0.04725744, 0.77263783, 313.23810451, 170.95427630, 74.01692503),
                    new KeplerianValueModel(-0.00196176, -0.00004397, -0.00242939, 428.48202785, 0.40805281, 0.04240589)),
                new KeplerianModel(
                    CelestialObject.Neptune,
                    new KeplerianValueModel(30.06992276, 0.00859048, 1.77004347, -55.12002969, 44.96476227, 131.78422574),
                    new KeplerianValueModel(0.00026291, 0.00005105, 0.00035372, 218.45945325, -0.32241464, -0.00508664)),
                new KeplerianModel(
                    CelestialObject.Pluto,
                    new KeplerianValueModel(39.48211675, 0.24882730, 17.14001206, 238.92903833, 224.06891629, 110.30393684),
                    new KeplerianValueModel(-0.00031596, 0.00005170, 0.00004818, 145.20780515, -0.04062942, -0.01183482))
            };
    }
}