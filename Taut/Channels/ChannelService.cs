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

            return ApiCallHelper(INFO_METHOD,
                    new { channel = channelId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChannelInfoResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChannelListResponse> List(bool excludeArchived = false)
        {
            return ApiCallHelper(LIST_METHOD,
                    new { exclude_archived = Convert.ToInt32(excludeArchived) },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChannelListResponse>(requestUrl, cancellationToken));
        }

        private IObservable<T> ApiCallHelper<T>(string method, object queryParams, Func<string, CancellationToken, Task<T>> httpCall)
        {
            return Observable.Create<T>(async (observer, cancellationToken) =>
            {
                var requestUrl = BuildRequestUrl(method, queryParams);
                observer.OnNext(await httpCall.Invoke(requestUrl, cancellationToken));
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}
