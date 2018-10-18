namespace MyTelescope.App.DataLayer.Models.Http
{
    using SWE.Http.Enums;
    using Interfaces;
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpDataExchanger : IDataExchanger<IRequestModel>, IDisposable
    {
        private readonly HttpClient _client = new HttpClient();

        private string ApiFormat { get; }

        private string ContentType { get; }

        public HttpDataExchanger(string url, string apiFormat, string contentType)
        {
            ApiFormat = apiFormat;
            ContentType = contentType;

            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(contentType));
        }

        public virtual async Task<HttpResponseMessage> RequestContent(IRequestModel requestModel)
        {
            switch (requestModel.Verb)
            {
                case HttpVerb.Get:
                    return await _client.GetAsync(string.Format(ApiFormat, requestModel.ApiRouteName, requestModel.ApiActionName) + requestModel.Content).ConfigureAwait(false);

                case HttpVerb.Post:
                    var contentResult = new StringContent(requestModel.Content, Encoding.UTF8, ContentType);
                    return await _client.PostAsync(string.Format(ApiFormat, requestModel.ApiRouteName, requestModel.ApiActionName), contentResult).ConfigureAwait(false);

                default:
                    return null;
            }
        }

        public async Task<byte[]> GetBytes(IRequestModel requestModel)
        {
            var response = await RequestContent(requestModel).ConfigureAwait(false);

            return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        public async Task<Stream> GetStream(IRequestModel requestModel)
        {
            var response = await RequestContent(requestModel).ConfigureAwait(false);

            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public async Task<string> GetString(IRequestModel requestModel)
        {
            var response = await RequestContent(requestModel).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~HttpDataExchanger()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        /// <summary>
        /// Disposing class
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _client?.Dispose();
            }
        }
    }
}