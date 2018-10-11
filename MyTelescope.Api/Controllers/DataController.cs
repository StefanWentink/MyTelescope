﻿namespace MyTelescope.Api.Controllers
{
    using Binders;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using SWE.Model.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Utilities.Interfaces.Connector;
    using Utilities.Models.Filter;
    using Utilities.Models.Sort;

    [Route("api/[controller]/[action]")]
    public abstract class DataController<TModel> : Controller
        where TModel : class, IKey
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

        // POST api/<controller>/get
        [HttpPost]
        public IEnumerable<TModel> Get([ModelBinder(BinderType = typeof(JsonModelBinder))] string filterString)
        {
            var taskResult = GetAsync(filterString);
            return taskResult.Result;
        }

        // POST api/<controller>/getAsync
        [HttpPost]
        public async Task<IEnumerable<TModel>> GetAsync([ModelBinder(BinderType = typeof(JsonModelBinder))] string filterString)
        {
            FilterModel filter = null;
            if (!string.IsNullOrEmpty(filterString))
            {
                try
                {
                    filter = JsonConvert.DeserializeObject<FilterModel>(filterString);
                }
                catch (Exception exception)
                {
                    throw new ArgumentException($"Unable to deserialize '{filterString}'. {exception.Message}", nameof(filterString));
                }
            }

            if (filter == null)
            {
                throw new ArgumentException($"no filter supplied: '{filterString}'", nameof(filterString));
            }

            return await Connector.ReadAsync(filter).ConfigureAwait(false);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<TModel> GetAsync(Guid id)
        {
            var result = await Connector.ReadAsync(x => x.Id == id, new SortModel(nameof(IKey.Id))).ConfigureAwait(false);
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