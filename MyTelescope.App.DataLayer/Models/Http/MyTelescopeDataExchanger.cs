namespace MyTelescope.App.DataLayer.Models.Http
{
    using Constants;
    using Interfaces;

    public class MyTelescopeDataExchanger : HttpDataExchanger, ICrudDataExchanger<IRequestModel>
    {
        public string CreateAction { get; }

        public string ReadAction { get; }

        public string UpdateAction { get; }

        public string DeleteAction { get; }

        public MyTelescopeDataExchanger()
            : base(Utilities.Helpers.ConfigHelper.GetConfigBuilder().Infrastructure.MyTeleScopeApi, "api/{0}/{1}", "application/json")
        {
            CreateAction = MyTelescopeActionConstants.Create;
            ReadAction = MyTelescopeActionConstants.Read;
            UpdateAction = MyTelescopeActionConstants.Update;
            DeleteAction = MyTelescopeActionConstants.Delete;
        }
    }
}
