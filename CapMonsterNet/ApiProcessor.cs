using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using CapMonsterNet.Models.Requests;
using CapMonsterNet.Exceptions;

namespace CapMonsterNet
{
    internal class ApiProcessor : IDisposable
    {
        private HttpClientHandler httpClientHandler;
        private HttpClient httpClient;

        private bool disposed = false;

        private static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public string Key { get; set; }

        public ApiProcessor()
        {
            httpClientHandler = new HttpClientHandler()
            {
                UseCookies = false,
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    httpClientHandler.Dispose();
                    httpClient.Dispose();
                }

                disposed = true;
            }
        }

        ~ApiProcessor()
        {
            Dispose(false);
        }

        private async Task<HttpResponseMessage> CreateRequestAsync(EndpointData endpoint, BaseRequest requestObject)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();

            // Auth
            if (endpoint.SecurityType == EndpointSecurityType.ApiKey)
            {
                if (requestObject != null)
                {
                    if (requestObject.ClientKey == null)
                    {
                        requestObject.ClientKey = Key;
                    }
                }
            }

            if (endpoint.Method == HttpMethod.Get)
            {
                UriBuilder uriBuilder = new UriBuilder(endpoint.Uri);
                if (requestObject != null)
                {
                    uriBuilder.Query = GenerateQueryString(requestObject);
                }
                requestMessage.RequestUri = uriBuilder.Uri;
            }
            else
            {
                requestMessage.RequestUri = endpoint.Uri;
                if (requestObject != null)
                {
                    string content = JsonConvert.SerializeObject(requestObject, SerializerSettings);
                    requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
                }
            }

            requestMessage.Method = endpoint.Method;

            HttpResponseMessage response;
            try
            {
                response = await httpClient.SendAsync(requestMessage);
                requestMessage.Dispose();
                return response;
            }
            catch (Exception)
            {
                requestMessage.Dispose();
                throw;
            }
        }

        public async Task<T> ProcessRequestAsync<T>(EndpointData endpoint, BaseRequest requestObject = null) where T : class
        {
            HttpResponseMessage message = await CreateRequestAsync(endpoint, requestObject);
            return await HandleJsonResponseAsync<T>(message);
        }

        private static async Task<T> HandleJsonResponseAsync<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = await responseMessage.Content.ReadAsStringAsync();

                T obj;
                try
                {
                    obj = JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception e)
                {
                    responseMessage.Dispose();
                    throw new ApiException("Unable to deserialize response message.", e);
                }

                if (obj == null)
                {
                    responseMessage.Dispose();
                    throw new ApiException("Unable to deserialize response message.");
                }

                return obj;
            }

            var body = await responseMessage.Content.ReadAsStringAsync();
            responseMessage.Dispose();
            throw CreateApiException(responseMessage.StatusCode, body);
        }

        private static ApiException CreateApiException(HttpStatusCode statusCode, string message)
        {
            return new ApiException(message);
        }

        private static String GenerateQueryString(BaseRequest requestObject)
        {
            if (requestObject == null)
            {
                throw new ArgumentNullException(nameof(requestObject));
            }

            JObject obj = JObject.FromObject(requestObject, JsonSerializer.Create(SerializerSettings));
            return String.Join("&", obj.Children()
                .Cast<JProperty>()
                .Where(j => j.Value != null)
                .Select(j => j.Name + "=" + WebUtility.UrlEncode(j.Value.ToString())));
        }
    }
}
