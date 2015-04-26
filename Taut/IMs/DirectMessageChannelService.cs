using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.IMs
{
    public class DirectMessageChannelService : BaseAuthenticatedApiService, IDirectMessageChannelService
    {
        private const string LIST_METHOD = "im.list";

        public DirectMessageChannelService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<DirectMessageChannelListResponse> List()
        {
            return ObservableApiCall(LIST_METHOD, null,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<DirectMessageChannelListResponse>(requestUrl, cancellationToken));
        }
    }
}
