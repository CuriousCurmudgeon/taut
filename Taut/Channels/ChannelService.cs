using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.Channels
{
    public class ChannelService : BaseAuthenticatedApiService, IChannelService
    {
        private const string INFO_METHOD = "channels.info";

        public ChannelService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<ChannelInfoResponse> Info(string channelId)
        {
            channelId.ThrowIfNull("channelId");

            return Observable.Create<ChannelInfoResponse>(async (observer, cancellationToken) =>
            {
                var requestUrl = BuildRequestUrl(INFO_METHOD, new { channel = channelId });
                observer.OnNext(await GetResponseAsync<ChannelInfoResponse>(requestUrl, cancellationToken));
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}
