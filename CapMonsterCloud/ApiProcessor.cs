using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using CapMonsterCloud.Models.Requests;
using CapMonsterCloud.Exceptions;


namespace CapMonsterCloud
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

        private Task<HttpResponseMessage> CreateRequestAsync(EndpointData endpoint, BaseRequest requestObject, CancellationToken cancellationToken)
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

            var task = httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            task.ContinueWith(_ => requestMessage.Dispose());
            return task;
        }

        public Task<T> ProcessRequestAsync<T>(EndpointData endpoint) where T : class
        {
            return ProcessRequestAsync<T>(endpoint, null, CancellationToken.None);
        }

        public Task<T> ProcessRequestAsync<T>(EndpointData endpoint, BaseRequest requestObject = null) where T : class
        {
            return ProcessRequestAsync<T>(endpoint, requestObject, CancellationToken.None);
        }

        public async Task<T> ProcessRequestAsync<T>(EndpointData endpoint, BaseRequest requestObject, CancellationToken cancellationToken) where T : class
        {
            var message = await CreateRequestAsync(endpoint, requestObject, cancellationToken).ConfigureAwait(false);
            return await HandleJsonResponseAsync<T>(message).ConfigureAwait(false);
        }

        private static async Task<T> HandleJsonResponseAsync<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
                T obj;
                try
                {
                    using (var streamReader = new StreamReader(jsonStream))
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        var serializer = new JsonSerializer();
                        obj = serializer.Deserialize<T>(jsonReader);
                    }
                }
                catch (Exception e)
                {
                    throw new ApiException("Unable to deserialize response message.", e);
                }
                finally
                {
                    responseMessage.Dispose();
                }

                if (obj == null)
                {
                    throw new ApiException("Unable to deserialize response message.");
                }

                return obj;
            }

            var body = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw CreateApiException(responseMessage.StatusCode, body);
        }

        private static ApiException CreateApiException(HttpStatusCode statusCode, string message)
        {
            return new ApiException(message);
        }

        private static string GenerateQueryString(BaseRequest requestObject)
        {
            if (requestObject == null)
            {
                throw new ArgumentNullException(nameof(requestObject));
            }

            JObject obj = JObject.FromObject(requestObject, JsonSerializer.Create(SerializerSettings));
            return string.Join("&", obj.Children()
                .Cast<JProperty>()
                .Where(j => j.Value != null)
                .Select(j => j.Name + "=" + WebUtility.UrlEncode(j.Value.ToString())));
        }
    }
}
