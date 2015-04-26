using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.Users
{
    public class UserService : BaseAuthenticatedApiService, IUserService
    {
        private const string LIST_METHOD = "users.list";

        public UserService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<UserListResponse> List()
        {
            return ObservableApiCall(LIST_METHOD, null,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<UserListResponse>(requestUrl, cancellationToken));
        }
    }
}
