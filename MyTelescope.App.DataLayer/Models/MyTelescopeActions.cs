namespace MyTelescope.App.DataLayer.Models
{
    using SWE.Http.Models;

    public class MyTelescopeActions : Actions
    {
        public MyTelescopeActions()
        : base("Create", "Read", "Update", "Delete")
        { }
    }
}