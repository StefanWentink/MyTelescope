namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public abstract class DataController<T> : Controller
        where T : class, IKeyModel
    {
        private IContextConnector<TModel> Connector { get; }

        protected DataController(IContextConnector<TModel> connector)
        {
            Connector = connector;
        }

        [HttpGet]
        public string Hello()
        {
            return $"Hello {nameof(TModel)} controller is here!";
        }

    {
    }
}