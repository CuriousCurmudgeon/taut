using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Authorizations
{
    public class OAuthService : BaseApiService, IOAuthService
    {
        private const string AUTHORIZE_URL = "https://slack.com/";
        private const string OAUTH_PATH = "/oauth/authorize";
        private const string ACCESS_METHOD = "oauth.access";

        public OAuthService(string clientId, string clientSecret)
        {
            clientId.ThrowIfNull("clientId");
            clientSecret.ThrowIfNull("clientSecret");

            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public string ClientId { get; private set; }

        public string ClientSecret { get; private set; }

        public Uri BuildOAuthUri(string state, AuthScopes scope, Uri redirectUri = null, string teamId = null)
        {
            state.ThrowIfNull("state");

            var queryParams = new Dictionary<string, object>
            {
                { "client_id", ClientId },
                { "state", state },
                { "redirect_uri", redirectUri },
                { "team", teamId },
                { "scope",  ScopesToString(scope) },
            };

            return new Uri(AUTHORIZE_URL
                .AppendPathSegment(OAUTH_PATH)
                .SetQueryParams(queryParams));
        }

        public IObservable<Authorization> Access(string code, Uri redirectUri = null)
        {
            code.ThrowIfNull("code");

            var queryParams = new
            {
                client_id = ClientId,
                client_secret = ClientSecret,
                code = code,
                redirect_uri = redirectUri,
            };
            return ObservableApiCall(ACCESS_METHOD, queryParams,
                async (requestUrl, cancellationToken) =>
                {
                    var response = await requestUrl.GetAsync(cancellationToken);
                    var responseContent = (response.Content == null) ? null : await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Authorization>(responseContent);
                });
        }

        #region Helpers

        private string ScopesToString(AuthScopes scope)
        {
            var scopes = new List<string>();
            foreach (Enum value in Enum.GetValues(typeof(AuthScopes)))
            {
                if (scope.HasFlag(value))
                {
                    scopes.Add(((AuthScopes)value).ToString().ToLower());
                }
            }
            return string.Join(",", scopes);
        }

        #endregion
    }
}
