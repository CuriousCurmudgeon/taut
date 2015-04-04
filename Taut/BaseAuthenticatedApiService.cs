using Flurl;
using System;
using Taut.Authorizations;

namespace Taut
{
    public abstract class BaseAuthenticatedApiService : BaseApiService
    {
        protected BaseAuthenticatedApiService(IUserCredentialService userCredentialService)
        {
            userCredentialService.ThrowIfNull("userCredentialService");

            UserCredentialService = userCredentialService;
        }

        public override Url BuildRequestUrl(string path, object queryParams)
        {
            if (!UserCredentialService.IsAuthorized)
            {
                throw new UserNotAuthenticatedException();
            }
            var accessToken = UserCredentialService.Authorization.AccessToken;
            return base.BuildRequestUrl(path, queryParams)
                .SetQueryParam("token", accessToken);
        }

        public IUserCredentialService UserCredentialService { get; private set; }
    }

    public class UserNotAuthenticatedException : Exception { }
}
