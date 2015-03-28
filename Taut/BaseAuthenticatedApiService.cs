using Flurl;
using System;
using Taut.Authorization;

namespace Taut
{
    internal abstract class BaseAuthenticatedApiService : BaseApiService
    {
        public BaseAuthenticatedApiService(IUserCredentialService userCredentialService)
        {
            userCredentialService.ThrowIfNull("userCredentialService");

            UserCredentialService = userCredentialService;
        }

        public override Url BuildRequestUrl(string method, object queryParams)
        {
            if (!UserCredentialService.IsAuthorized)
            {
                throw new UserNotAuthenticatedException();
            }
            var accessToken = UserCredentialService.GetAuthorization().AccessToken;
            return base.BuildRequestUrl(method, queryParams)
                .SetQueryParam("token", accessToken);
        }

        public IUserCredentialService UserCredentialService { get; private set; }
    }

    public class UserNotAuthenticatedException : Exception { }
}
