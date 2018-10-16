namespace MyTelescope.OData.Controllers
{
    using Microsoft.AspNet.OData;
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.Utilities.Interfaces.Connector;
    using SWE.Model.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public abstract class DataController<T> : ODataController
        where T : class, IKey
    {
        private IContextConnector<T> Connector { get; }

        protected DataController(IContextConnector<T> connector)
        {
            Connector = connector;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(Connector.Queryable);
        }

        [EnableQuery]
        public IActionResult Get([FromODataUri] Guid key)
        {
            return Ok(Connector.Queryable.SingleOrDefault(x => x.Id == key));
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]T item)
        {
            await Connector.CreateAsync(item).ConfigureAwait(false);
            return Created(item);
        }
    }
}