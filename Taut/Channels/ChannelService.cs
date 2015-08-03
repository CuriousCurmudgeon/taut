using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taut;
using Taut.Authorizations;

namespace Taut.Channels
{
    public class ChannelService : BaseAuthenticatedApiService, IChannelService
    {
        private const string ARCHIVE_METHOD = "channels.archive";
        private const string CREATE_METHOD = "channels.create";
        private const string HISTORY_METHOD = "channels.history";
        private const string INFO_METHOD = "channels.info";
        private const string INVITE_METHOD = "channels.invite";
        private const string JOIN_METHOD = "channels.join";
        private const string LIST_METHOD = "channels.list";

        public ChannelService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<BaseResponse> Archive(string channelId)
        {
            channelId.ThrowIfNull("channelId");

            return ObservableApiCall(ARCHIVE_METHOD,
                    new { channel = channelId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<BaseResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChannelResponse> Create(string name)
        {
            name.ThrowIfNullOrEmpty("name");

            return ObservableApiCall(CREATE_METHOD,
                    new { name = name },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChannelResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChannelHistoryResponse> History(string channelId, double? latest = null,
            double? oldest = null, bool? isInclusive = null, int? count = null)
        {
            channelId.ThrowIfNull("channelId");

            var queryParams = new Dictionary<string, object>
            {
                { "channel", channelId },
                { "latest", latest },
                { "oldest", oldest },
                { "inclusive", isInclusive.ToInt32() },
                { "count", count }
            };

            return ObservableApiCall(HISTORY_METHOD, queryParams,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChannelHistoryResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChannelResponse> Info(string channelId)
        {
            channelId.ThrowIfNull("channelId");

            return ObservableApiCall(INFO_METHOD,
                    new { channel = channelId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChannelResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChannelResponse> Invite(string channelId, string userId)
        {
            channelId.ThrowIfNull("channelId");
            userId.ThrowIfNull("userId");

            return ObservableApiCall(INVITE_METHOD,
                    new { channel = channelId, user = userId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChannelResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChannelResponse> Join(string name)
        {
            name.ThrowIfNullOrEmpty("name");

            return ObservableApiCall(JOIN_METHOD,
                    new { name = name },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChannelResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChannelListResponse> List(bool excludeArchived = false)
        {
            return ObservableApiCall(LIST_METHOD,
                    new { exclude_archived = Convert.ToInt32(excludeArchived) },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChannelListResponse>(requestUrl, cancellationToken));
        }
    }
}
