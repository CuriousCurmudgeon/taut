using Flurl;
using Taut.Authorization;

namespace Taut
{
    internal abstract class BaseAuthenticatedApiService : BaseApiService
    {
        public BaseAuthenticatedApiService(IUserCredentialService userCredentialService)
        {
            UserCredentialService = userCredentialService;
        }

        public override Url BuildRequestUrl(string method, object queryParams)
        {
            var accessToken = UserCredentialService.GetAuthorization().AccessToken;
            return base.BuildRequestUrl(method, queryParams)
                .SetQueryParam("token", accessToken);
        }

        public IUserCredentialService UserCredentialService { get; private set; }
    }
}
