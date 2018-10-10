namespace MyTelescope.Seeder.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using SolarSystem.Constants;
    using SolarSystem.Enums;
    using SolarSystem.Extensions;
    using SolarSystem.Models.CelestialObject;
    using Utilities.Interfaces.Connector;

    public class CelestialObjectMoonSeeder : BaseSeeder<CelestialObjectModel, string>
    {
        private static Dictionary<string, Guid> CelestialObjectTypeDictionary { get; set; }

        private static Dictionary<string, Guid> CelestialObjectDictionary { get; set; }

        public CelestialObjectMoonSeeder(
            IContextConnector<CelestialObjectModel> connector,
            List<CelestialObjectTypeModel> celestialObjectTypes,
            List<CelestialObjectModel> celestialObjects)
            : base(connector)
        {
            CelestialObjectTypeDictionary = celestialObjectTypes.ToDictionary(x => x.Code, x => x.Id);
            CelestialObjectDictionary = celestialObjects.ToDictionary(x => x.Code, x => x.Id);
        }

        private static List<CelestialObjectModel> List => new List<CelestialObjectModel>
            {
                new CelestialObjectModel(
                    CelestialObject.Moon.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#bcb49b",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b3/Full_moon.jpeg/532px-Full_moon.jpeg",
                    0.07346,
                    27.322,
                    655.728,
                    3344,
                    1738.1,
                    1736.0,
                    0.3844,
                    CelestialObjectDictionary[CelestialObjectConstants.Earth]),

                new CelestialObjectModel(
                    CelestialObject.Phobos.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#dfbe6f",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/ce/Phobos_moon_%28large%29.jpg/532px-Phobos_moon_%28large%29.jpg",
                    10.6 / Math.Pow(10, 9),
                    0.31891,
                    0.31891,
                    1900,
                    13,
                    9.1,
                    9378 / Math.Pow(10, 6),
                    CelestialObjectDictionary[CelestialObjectConstants.Mars]),

                new CelestialObjectModel(
                    CelestialObject.Deimos.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#f5ebd3",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4a/Deimos2.jpg/532px-Deimos2.jpg",
                    2.4 / Math.Pow(10, 9),
                    1.26244,
                    1.26244,
                    1750,
                    7.8,
                    5.1,
                    23459 / Math.Pow(10, 6),
                    CelestialObjectDictionary[CelestialObjectConstants.Mars]),

                new CelestialObjectModel(
                    CelestialObject.Io.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#e2fd7a",
                    "https://upload.wikimedia.org/wikipedia/commons/1/14/Io%2C_moon_of_Jupiter%2C_NASA.jpg",
                    893.2 / Math.Pow(10, 4),
                    1.769138,
                    1.769138,
                    3530,
                    1821.5,
                    1821.5,
                    421.8 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Jupiter]),

                new CelestialObjectModel(
                    CelestialObject.Europa.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#DED4A5",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/54/Europa-moon.jpg/532px-Europa-moon.jpg",
                    480.0 / Math.Pow(10, 4),
                    3.551181,
                    3.551181,
                    3010,
                    1560.8,
                    1560.8,
                    671.1 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Jupiter]),

                new CelestialObjectModel(
                    CelestialObject.Ganymede.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#91826F",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/Ganymede%2C_moon_of_Jupiter%2C_NASA.jpg/532px-Ganymede%2C_moon_of_Jupiter%2C_NASA.jpg",
                    1481.9 / Math.Pow(10, 4),
                    7.154553,
                    7.154553,
                    1940,
                    2631.2,
                    2631.2,
                    1070.4 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Jupiter]),

                new CelestialObjectModel(
                    CelestialObject.Callisto.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#6D6455",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Callisto.jpg/532px-Callisto.jpg",
                    1075.9 / Math.Pow(10, 4),
                    16.689017,
                    16.689017,
                    1830,
                    2410.3,
                    2410.3,
                    1882.7 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Jupiter]),

                new CelestialObjectModel(
                    CelestialObject.Mimas.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#AFAFAF",
                    "https://upload.wikimedia.org/wikipedia/commons/d/da/Mimas_moon.jpg",
                    0.379 / Math.Pow(10, 4),
                    0.9424218,
                    0.9424218,
                    1150,
                    208,
                    191,
                    185.52 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Saturn]),

                new CelestialObjectModel(
                    CelestialObject.Enceladus.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#EAE3EB",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/5/5f/Enceladusstripes_cassini.jpg/532px-Enceladusstripes_cassini.jpg",
                    1.08 / Math.Pow(10, 4),
                    1.370218,
                    1.370218,
                    1610,
                    257,
                    248,
                    238.02 / Math.Pow(10, 3),
                CelestialObjectDictionary[CelestialObjectConstants.Saturn]),

                new CelestialObjectModel(
                    CelestialObject.Tethys.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#C4BDAD",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/aa/Saturn%27s_Moon_Tethys_as_seen_from_Voyager_2.jpg/532px-Saturn%27s_Moon_Tethys_as_seen_from_Voyager_2.jpg",
                    6.18 / Math.Pow(10, 4),
                    1.887802,
                    1.887802,
                    985,
                    538,
                    526,
                    294.66 / Math.Pow(10, 3),
                CelestialObjectDictionary[CelestialObjectConstants.Saturn]),

                new CelestialObjectModel(
                    CelestialObject.Dione.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#888A64",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c7/Dione_color.jpg/532px-Dione_color.jpg",
                    11 / Math.Pow(10, 4),
                    2.736915,
                    2.736915 * 24,
                    1480,
                    563,
                    560,
                    377.40 / Math.Pow(10, 3),
                CelestialObjectDictionary[CelestialObjectConstants.Saturn]),

                new CelestialObjectModel(
                    CelestialObject.Rhea.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#CAC3BD",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Rhea_true_color.jpg/532px-Rhea_true_color.jpg",
                    23.1 / Math.Pow(10, 4),
                    4.517500,
                    4.517500 * 24,
                    1240,
                    765,
                    762,
                    527.04 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Saturn]),

                new CelestialObjectModel(
                    CelestialObject.Titan.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#E1CA56",
                    "https://upload.wikimedia.org/wikipedia/commons/d/dd/Titan_Visible.jpg",
                    1345.5 / Math.Pow(10, 4),
                    15.945421,
                    15.945421 * 24,
                    1880,
                    2575,
                    2575,
                    1221.83 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Saturn]),

                new CelestialObjectModel(
                    CelestialObject.Hyperion.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#4F4E4C",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/6/68/Hyperion_PIA07740.jpg/532px-Hyperion_PIA07740.jpg",
                    0.056 / Math.Pow(10, 4),
                    21.276609,
                    21.276609 * 24,
                    550,
                    180,
                    103,
                    1481.1 / Math.Pow(10, 3),
                CelestialObjectDictionary[CelestialObjectConstants.Saturn]),

                new CelestialObjectModel(
                    CelestialObject.Iapetus.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#CECBB8",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c9/Iapetus_as_seen_by_the_Cassini_probe_-_20071008.jpg/532px-Iapetus_as_seen_by_the_Cassini_probe_-_20071008.jpg",
                    18.1 / Math.Pow(10, 4),
                    79.330183,
                    79.330183 * 24,
                    1090,
                    746,
                    712,
                    3561.3 / Math.Pow(10, 3),
                CelestialObjectDictionary[CelestialObjectConstants.Saturn]),

                new CelestialObjectModel(
                    CelestialObject.Miranda.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#DCC8C9",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/2/28/Miranda3.jpg/532px-Miranda3.jpg",
                    0.66 / Math.Pow(10, 4),
                    1.413479,
                    1.413479 * 24,
                    1200,
                    240,
                    232.9,
                    129.90 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Uranus]),

                new CelestialObjectModel(
                    CelestialObject.Ariel.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#705D56",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Color_Image_of_Ariel_as_seen_from_Voyager_2.jpg/532px-Color_Image_of_Ariel_as_seen_from_Voyager_2.jpg",
                    12.9 / Math.Pow(10, 4),
                    2.520379,
                    2.520379 * 24,
                    1590,
                    581.1,
                    577.9,
                    199.90 / Math.Pow(10, 3),
                CelestialObjectDictionary[CelestialObjectConstants.Uranus]),

                new CelestialObjectModel(
                    CelestialObject.Umbriel.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#565656",
                    "https://upload.wikimedia.org/wikipedia/commons/5/50/Umbriel_%28moon%29.jpg",
                    12.2 / Math.Pow(10, 4),
                    4.144176,
                    4.144176 * 24,
                    1460,
                    584.7,
                    584.7,
                    266.00 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Uranus]),

                new CelestialObjectModel(
                    CelestialObject.Titania.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#7C7C7C",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4e/PIA00039_Titania.jpg/532px-PIA00039_Titania.jpg",
                    34.2 / Math.Pow(10, 4),
                    8.705867,
                    8.705867 * 24,
                    1660,
                    788.9,
                    788.9,
                    436.30 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Uranus]),

                new CelestialObjectModel(
                    CelestialObject.Oberon.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#9F9087",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/3/35/Color_Image_of_Oberon_as_seen_from_Voyager_2.jpg/532px-Color_Image_of_Oberon_as_seen_from_Voyager_2.jpg",
                    28.8 / Math.Pow(10, 4),
                    13.463234,
                    13.463234 * 24,
                    1560,
                    761.4,
                    761.4,
                    583.50 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Uranus]),

                new CelestialObjectModel(
                    CelestialObject.Proteus.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#D7D7D7",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/Proteus_%28Voyager_2%29.jpg/532px-Proteus_%28Voyager_2%29.jpg",
                    0.5 / Math.Pow(10, 4),
                    1.122315,
                    1.122315 * 24,
                    0,
                    220,
                    202,
                    117.647 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Neptune]),

                new CelestialObjectModel(
                    CelestialObject.Triton.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#CBC2BD",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Triton_moon_mosaic_Voyager_2_%28large%29.jpg/532px-Triton_moon_mosaic_Voyager_2_%28large%29.jpg",
                    214 / Math.Pow(10, 4),
                    -5.876854,
                    5.876854 * 24,
                    2050,
                    1353.4,
                    1353.4,
                    354.76 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Neptune]),

                new CelestialObjectModel(
                    CelestialObject.Nereid.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#999999",
                    "https://upload.wikimedia.org/wikipedia/commons/b/b0/Nereid-Voyager2.jpg",
                    0.3 / Math.Pow(10, 4),
                    360.13619,
                    360.13619 * 24,
                    0,
                    170,
                    170,
                    5513.4 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Neptune]),

                new CelestialObjectModel(
                    CelestialObject.Charon.ToConstant(),
                    CelestialObjectTypeDictionary[CelestialObjectTypeConstants.MajorMoon],
                    "#CBB8AA",
                    "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Charon_by_New_Horizons_on_13_July_2015.png/532px-Charon_by_New_Horizons_on_13_July_2015.png",
                    1.586 / Math.Pow(10, 3),
                    6.3872,
                    6.3872 * 24,
                    1700,
                    606,
                    606,
                    19.596 / Math.Pow(10, 3),
                    CelestialObjectDictionary[CelestialObjectConstants.Pluto]),
            };

        protected override List<Expression<Func<CelestialObjectModel, bool>>> GetBatchExpression()
        {
            return new List<Expression<Func<CelestialObjectModel, bool>>> { x => true };
        }

        protected override List<CelestialObjectModel> SeedList(Expression<Func<CelestialObjectModel, bool>> batchExpression)
        {
            return List.Where(batchExpression.Compile()).ToList();
        }

        protected override Func<CelestialObjectModel, string> DuplicateCheckFunction
        {
            get { return x => x.Code; }
        }
    }
}