using System.Collections.Generic;

namespace MyTelescope.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MyTelescope.Utilities.Interfaces;
    using MyTelescope.Utilities.Interfaces.Connector;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;

    [Route("api/[controller]")]
    public class DataController<TModel> : Controller //, IDataController<TModel>
        where TModel : class, IKeyModel
    {
        private IContextConnector<TModel> Connector { get; }

        public DataController(IContextConnector<TModel> connector)
        {
            Connector = connector;
        }

        // GET api/<controller>/get
        [HttpGet("Get")]
        public async Task<IEnumerable<TModel>> Get([FromQuery] FilterModel filter)
        {
            return await Connector.ReadAsync(filter);
        }

        // GET api/<controller>getAsync
        [HttpGet("GetAsync")]
        public async Task<IEnumerable<TModel>> GetAsync([FromQuery] FilterModel filter)
        {
            return await Connector.ReadAsync(filter);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<TModel> GetAsync(Guid id)
        {
            var result = await Connector.ReadAsync(x => x.Id == id, new SortModel(nameof(IKeyModel.Id)));
            return result.Single();
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
