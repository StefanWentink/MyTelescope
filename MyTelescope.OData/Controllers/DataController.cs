namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.Utilities.Interfaces.Connector;
    using SWE.Model.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public abstract class DataController<T> : Controller
        where T : class, IKey
    {
        private IContextConnector<T> Connector { get; }

        protected DataController(IContextConnector<T> connector)
        {
            Connector = connector;
        }

        [HttpGet]
        public string Hello()
        {
            return $"Hello {nameof(T)} controller is here!";
        }
    }
}