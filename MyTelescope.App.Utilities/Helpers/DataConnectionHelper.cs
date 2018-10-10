namespace MyTelescope.App.Utilities.Helpers
{
    using Enums;
    using System;
    using System.Linq;
    using Plugin.Connectivity;
    using Plugin.Connectivity.Abstractions;

    public static class DataConnectionHelper
    {
        private static readonly object DataConnectionLock = new object();

        private static DataConnection _dataConnection = DataConnection.Undetermined;

        private static readonly object InitDataConnectionLock = new object();

        private static bool initDataConnection;

        public static DataConnection GetDataConnection()
        {
            lock (DataConnectionLock)
            {
                return _dataConnection;
            }
        }

        public static void InitDataConnection()
        {
            lock(InitDataConnectionLock)
            {
                if (!initDataConnection)
                {
                    initDataConnection = true;
                    CrossConnectivity.Current.ConnectivityChanged += CurrentOnConnectivityChanged;
                    DetectAndSetDataConnection(CrossConnectivity.Current.IsConnected);
                }
            }
        }

        private static void CurrentOnConnectivityChanged(object sender, ConnectivityChangedEventArgs connectivityChangedEventArgs)
        {
            DetectAndSetDataConnection(connectivityChangedEventArgs.IsConnected);
        }

        private static void DetectAndSetDataConnection(bool isConnected)
        {
            if (!isConnected)
            {
                SetDataConnection(DataConnection.None);
                return;
            }

            foreach (var connectionType in CrossConnectivity.Current.ConnectionTypes)
            {
                switch (connectionType)
                {
                    case ConnectionType.WiFi:
                    case ConnectionType.Wimax:
                        SetDataConnection(DataConnection.Unlimited);
                        return;
                    default:
                        SetDataConnection(DataConnection.Limited);
                        return;
                }
            }
        }


        private static void SetDataConnection(DataConnection dataConnection)
        {
            lock (DataConnectionLock)
            {
                _dataConnection = dataConnection;
            }
        }

        public static int GetPositionDaysForDataConnection()
        {
            return GetPositionDaysForDataConnection(GetDataConnection());
        }

        internal static int GetPositionDaysForDataConnection(DataConnection dataConnection)
        {
            switch (DataConnectionHelper.GetDataConnection())
            {
                case DataConnection.None:
                    return 0;
                case DataConnection.Limited:
                    return 30;
                case DataConnection.Unlimited:
                    return 100;
                case DataConnection.Undetermined:
                    throw new ArgumentOutOfRangeException($"{nameof(DataConnection)} not yet determined", nameof(dataConnection));
                default:
                    throw new ArgumentOutOfRangeException($"{nameof(DataConnection)} not implemented", nameof(dataConnection));
            }
        }
    }
}
