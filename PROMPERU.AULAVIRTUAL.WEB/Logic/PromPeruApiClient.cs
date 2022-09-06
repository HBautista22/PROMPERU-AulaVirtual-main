using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PROMPERU.AULAVIRTUAL.WEB.Logic
{
    public class PromPeruApiClient : IDisposable
    {
        private readonly Uri BaseEndpoint;
        private readonly HttpClient _httpClient;

        public PromPeruApiClient()
        {
            string baseUri = ConfigurationManager.AppSettings.Get("promperu-serverapi");
            BaseEndpoint = new Uri(baseUri);
            _httpClient = new HttpClient();
        }

        /// <summary>  
        /// Common method for making POST calls  
        /// </summary>  
        public async Task<T> PostAsync<T>(string requestUrl, object content, CancellationToken cancellationToken)
        {
            AddHeaders();
            var response = await _httpClient.PostAsync(CreateRequestUri(requestUrl), CreateHttpContent(content), cancellationToken);
            HandleResponseCode(response);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>  
        /// Common method for making GET calls  
        /// </summary>  
        public async Task<T> GetAsync<T>(string requestUrl, string token, NameValueCollection query = null)
        {
            AddHeaders();
            var response = await _httpClient.GetAsync(CreateRequestUri(requestUrl, query), HttpCompletionOption.ResponseHeadersRead);
            HandleResponseCode(response);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(data);
        }

        public async Task<T1> PutAsync<T1>(string requestUrl, string token, NameValueCollection query = null)
        {
            AddHeaders();
            var response = await _httpClient.PutAsync(CreateRequestUri(requestUrl, query), null);
            HandleResponseCode(response);
            var data = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(data);
        }

        internal string GetTokenTransaction()
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var response = _httpClient.PostAsync(CreateRequestUri("api/login/authenticate"), CreateHttpContent(new { Username = "BITPERFECT", Password = "b1tp3rf3ct" })).Result;
            response.EnsureSuccessStatusCode();
            var data = response.Content.ReadAsStringAsync().Result;
            var tokenModel = JsonConvert.DeserializeObject<GenerateToken>(data);

            if (tokenModel.status != "200")
                throw new Exception(tokenModel.message);

            return tokenModel.message;
        }

        private void HandleResponseCode(HttpResponseMessage response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Error en la llamada a los servicios. Codigo : {response.StatusCode.ToString()}");
            }
        }

        private Uri CreateRequestUri(string relativePath, NameValueCollection query = null)
        {
            var endpoint = new Uri(BaseEndpoint, relativePath);

            var collection = HttpUtility.ParseQueryString(string.Empty);

            if (query != null)
                foreach (var key in query.Cast<string>().Where(key => !string.IsNullOrEmpty(query[key])))
                {
                    collection[key] = query[key];
                }

            var uriBuilder = new UriBuilder(endpoint)
            {
                Query = collection.ToString()
            };
            return uriBuilder.Uri;
        }

        private HttpContent CreateHttpContent(object content)
        {
            var json = JsonConvert.SerializeObject(content, MicrosoftDateFormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private HttpContent CreatHttpContnetText(string content)
        {
            return new StringContent(content);
        }

        private static JsonSerializerSettings MicrosoftDateFormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }

        /// <summary>
        /// Add any headers you need. Modify as your needs
        /// </summary>
        /// <param name="token"></param>
        private void AddHeaders()
        {
            var token = GetTokenTransaction();

            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }

        public void Dispose()
        {
            this._httpClient.Dispose();
        }
    }

    internal class GenerateToken
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}