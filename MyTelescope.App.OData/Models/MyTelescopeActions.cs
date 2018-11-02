namespace MyTelescope.App.OData.Models
{
    using SWE.Http.Models;

    public class MyTelescopeActions : Actions
    {
        public MyTelescopeActions()
        : base("Create", string.Empty, "Update", "Delete")
        {
        }
    }
}