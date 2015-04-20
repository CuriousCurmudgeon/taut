using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Authorizations
{
    [Flags]
    public enum AuthScopes
    {
        Identify = 1,
        Read = 2,
        Post = 4,
        Client = 8,
        Admin = 16,
    }

    /// <summary>
    /// <see cref="https://api.slack.com/docs/oauth"/>
    /// </summary>
    public interface IOAuthService
    {
        string ClientId { get; }

        string ClientSecret { get; }

        Uri BuildOAuthUri(string state, AuthScopes scope, Uri redirectUri = null, string teamId = null);

        Authorization Access(string code, Uri redirectUri = null);
    }
}
