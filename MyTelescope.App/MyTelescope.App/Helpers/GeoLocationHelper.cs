namespace MyTelescope.App.Helpers
{
    using Plugin.Geolocator;
    using Plugin.Geolocator.Abstractions;
    using System;
    using System.Threading.Tasks;
    using Utilities.Helpers;

    public static class GeoLocationHelper
    {
        private static Position _position;

        public static Position GetGeoLocation()
        {
            return _position ?? (_position = Task.Run(LoadGeoLocation).Result);
        }

        public static async Task SetGeoLocation()
        {
            _position = await LoadGeoLocation().ConfigureAwait(false);
        }

        private static async Task<Position> LoadGeoLocation()
        {
            try
            {
                var locator = CrossGeolocator.Current;

                return await locator.GetPositionAsync(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                LogHelper.LogError(exception);
                LogHelper.LogError($"{nameof(Position)} could not be retrieved.");
                return new Position(0, 0);
            }
        }
    }
}