using Newtonsoft.Json;

namespace Taut.Authorization
{
    public class Authorization
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public string Scope { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var auth = obj as Authorization;
            return AccessToken == auth.AccessToken
                && Scope == auth.Scope;
        }

        public override int GetHashCode()
        {
            return AccessToken.GetHashCode() + Scope.GetHashCode();
        }
    }
}
