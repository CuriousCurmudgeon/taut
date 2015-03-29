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
        private const string LIST_METHOD = "channels.list";

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

        public IObservable<ChannelListResponse> List(bool excludeArchived = false)
        {
            return Observable.Create<ChannelListResponse>(async (observer, cancellationToken) =>
            {
                var requestUrl = BuildRequestUrl(LIST_METHOD,
                    new
                    {
                        exclude_archived = Convert.ToInt32(excludeArchived),
                    });
                observer.OnNext(await GetResponseAsync<ChannelListResponse>(requestUrl, cancellationToken));
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}
