using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Authorizations
{
    public class OAuthService : BaseApiService, IOAuthService
    {
        private const string OAUTH_PATH = "/oauth/authorize";

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

            return new Uri(BuildRequestUrl(OAUTH_PATH, queryParams).ToString());
        }

        public Authorization Access(string code, Uri redirectUri = null)
        {
            throw new NotImplementedException();
        }

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
    }
}
