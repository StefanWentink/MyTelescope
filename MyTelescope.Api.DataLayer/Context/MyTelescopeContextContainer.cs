namespace MyTelescope.Api.DataLayer.Context
{
    using Ef.Utilities.Models;

    public class MyTelescopeContextContainer : ContextContainer
    {
        public MyTelescopeContextContainer()
            : base(new MyTelescopeContext())
        {
        }
    }
}
