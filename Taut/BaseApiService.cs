using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Taut
{
    public abstract class BaseApiService
    {
        public const string API_URL = "https://slack.com/api/";

        protected BaseApiService() { }

        #region BuildRequestUrl

        /// <summary>
        /// Appends path to <see cref="API_URL"/>.
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="ArgumentNullException">Thrown if path is null.</exception>
        /// <returns></returns>
        public virtual Url BuildRequestUrl(string path)
        {
            return BuildRequestUrl(path, new { });
        }

        /// <summary>
        /// Appends path and queryParams to <see cref="API_URL"/>.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="queryParams">Query string params. Can be null</param>
        /// <exception cref="ArgumentNullException">Thrown if path is null.</exception>
        /// <returns></returns>
        public virtual Url BuildRequestUrl(string path, object queryParams)
        {
            path.ThrowIfNull("path");

            return API_URL
                .AppendPathSegment(path)
                .SetQueryParams(queryParams);
        }

        #endregion

        /// <summary>
        /// Make a GET request for the url.
        /// </summary>
        /// <typeparam name="T">Type to deserialize response to.</typeparam>
        /// <param name="url"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Response content deserializd to T.</returns>
        public async Task<T> GetResponseAsync<T>(Url url, CancellationToken cancellationToken) where T : BaseResponse
        {
            url.ThrowIfNull("url");

            var response = await url.GetAsync(cancellationToken);
            var responseContent = (response.Content == null) ? null : await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(responseContent);
            if (!result.Ok)
            {
                throw new SlackApiException(result.Error);
            }
            return result;
        }

        protected IObservable<T> ObservableApiCall<T>(string method, object queryParams, Func<string, CancellationToken, Task<T>> httpCall)
        {
            return Observable.Create<T>(async (observer, cancellationToken) =>
            {
                var requestUrl = BuildRequestUrl(method, queryParams);
                observer.OnNext(await httpCall.Invoke(requestUrl, cancellationToken));
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}
