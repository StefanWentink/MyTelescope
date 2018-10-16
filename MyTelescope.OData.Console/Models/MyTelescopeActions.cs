namespace MyTelescope.OData.Console.Models
{
    using SWE.Http.Models;

    public class MyTelescopeActions : Actions
    {
        public MyTelescopeActions()
        : base("Create", string.Empty, "Update", "Delete")
        { }
    }
}