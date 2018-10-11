namespace MyTelescope.Test.Integration
{
    using App.DataLayer.Models;
    using App.DataLayer.Models.Http;
    using App.Test.Base;
    using Core.Utilities.Helpers.Config;
    using MyTelescope.SolarSystem.Models.CelestialObject;
    using MyTelescope.Utilities.Enums;
    using MyTelescope.Utilities.Helpers;
    using MyTelescope.Utilities.Models.Filter;
    using MyTelescope.Utilities.Models.Sort;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using Xunit;

    public class ApiTest : IClassFixture<CustomFixture>
    {
        [Fact]
        public void ApiValuesTest()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ConfigHelper.ApiMyTelescope)
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var task = client.GetAsync("api/values");
                var response = task.Result;
                var contentTask = response.Content.ReadAsStringAsync();
                Assert.True(contentTask.Result.Contains("["), "Invalid result");
            }
            catch (Exception exception)
            {
                Assert.True(string.IsNullOrEmpty(exception.Message), exception.Message);
            }
        }

        [Fact]
        public void ApiGetCelestialObjectTypeTest()
        {
            var modelName = ModelHelper.GetName(nameof(CelestialObjectTypeModel));
            AssertExecuteTest<CelestialObjectTypeModel>(modelName);
        }

        [Fact]
        public void ApiGetCelestialObjectTest()
        {
            var modelName = ModelHelper.GetName(nameof(CelestialObjectModel));
            AssertExecuteTest<CelestialObjectModel>(modelName);
        }

        [Fact]
        public void ApiGetCelestialObjectPositionTest()
        {
            var modelName = ModelHelper.GetName(nameof(CelestialObjectPositionModel));
            AssertExecuteTest<CelestialObjectPositionModel>(modelName);
        }

        private void AssertExecuteTest<T>(string modelName)
            where T : class
        {
            var contentResult = ExecuteBasicHttpRequest(modelName);
            var result = JsonConvert.DeserializeObject<List<T>>(contentResult);
            Assert.Equal(3, result.Count);

            contentResult = ExecuteExchangerRequest(modelName);
            result = JsonConvert.DeserializeObject<List<T>>(contentResult);
            Assert.Equal(3, result.Count);

            result = ExecuteDataTransponderRequest<T>().ToList();
            Assert.Equal(3, result.Count);
        }

        private IEnumerable<T> ExecuteDataTransponderRequest<T>()
        where T : class
        {
            var filter = GetFilter();
            var datatransponder = new DataTransponder(new MyTelescopeDataExchanger());
            return datatransponder.Read<T>(filter);
        }

        private string ExecuteExchangerRequest(string modelName)
        {
            var filter = GetFilter();

            var exchanger = new MyTelescopeDataExchanger();
            var requestModel = new MyTelescopeRequestModel(modelName, exchanger.ReadAction, filter);

            try
            {
                var contentTask = exchanger.GetString(requestModel);
                var contentResult = contentTask.Result;
                Assert.True(contentResult.Contains("["), "Invalid result");
                return contentResult;
            }
            catch (Exception exception)
            {
                Assert.True(string.IsNullOrEmpty(exception.Message), exception.Message);
                throw;
            }
        }

        private string ExecuteBasicHttpRequest(string modelName)
        {
            try
            {
                var filter = GetFilter();

                var contentString = JsonConvert.SerializeObject(filter);
                var content = new StringContent(contentString, Encoding.UTF8, "application/json");

                string contentResult;

                const string actionName = "Get";
                var url = $"api/{modelName}/{actionName}";
                var uri = new Uri(ConfigHelper.ApiMyTelescope + url);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    var task = client.PostAsync(uri, content);

                    var response = task.Result;
                    var contentTask = response.Content.ReadAsStringAsync();
                    contentResult = contentTask.Result;
                }

                Assert.True(contentResult.Contains("["), "Invalid result");

                return contentResult;
            }
            catch (Exception exception)
            {
                Assert.True(string.IsNullOrEmpty(exception.Message), exception.Message);
                throw;
            }
        }

        private static FilterModel GetFilter()
        {
            var sort = new SortModel("Id", 1, 3);
            var filterItem = new FilterItemModel("Id", ColumnType.GuidColumn, FilterType.NotEqual, Guid.NewGuid());
            return new FilterModel(sort, filterItem);
        }
    }
}