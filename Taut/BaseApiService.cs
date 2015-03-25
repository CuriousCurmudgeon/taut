using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Taut
{
    internal abstract class BaseApiService
    {
        public const string API_URL = "https://slack.com/api/";

        protected BaseApiService() { }

        public virtual Url BuildRequestUrl(string method)
        {
            return BuildRequestUrl(method, new { });
        }

        public virtual Url BuildRequestUrl(string method, object queryParams)
        {
            method.ThrowIfNull("method");

            return API_URL
                .AppendPathSegment(method)
                .SetQueryParams(queryParams);
        }

        protected async Task<T> GetResponseAsync<T>(Url url) where T : new()
        {
            var response = await url.GetAsync();
            var responseContent = (response.Content == null) ? null : await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }
    }
}
