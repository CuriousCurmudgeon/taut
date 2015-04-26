using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.RealTime
{
    public class RealTimeMessagingService : BaseAuthenticatedApiService, IRealTimeMessagingService
    {
        private const string START_METHOD = "rtm.start";

        public RealTimeMessagingService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<RealTimeMessagingStartResponse> Start()
        {
            return ObservableApiCall(START_METHOD, null,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<RealTimeMessagingStartResponse>(requestUrl, cancellationToken));
        }
    }
}
