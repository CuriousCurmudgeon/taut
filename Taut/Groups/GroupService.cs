﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.Groups
{
    public class GroupService : BaseAuthenticatedApiService, IGroupService
    {
        private const string ARCHIVE_METHOD = "groups.archive";
        private const string CLOSE_METHOD = "groups.close";
        private const string CREATE_METHOD = "groups.create";
        private const string CREATE_CHILD_METHOD = "groups.createChild";
        private const string HISTORY_METHOD = "groups.history";
        private const string INFO_METHOD = "groups.info";

        public GroupService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<BaseResponse> Archive(string channelId)
        {
            channelId.ThrowIfNull("channelId");

            return ObservableApiCall(ARCHIVE_METHOD,
                    new { channel = channelId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<BaseResponse>(requestUrl, cancellationToken));
        }

        public IObservable<BaseResponse> Close(string channelId)
        {
            channelId.ThrowIfNull("channelId");

            return ObservableApiCall(CLOSE_METHOD,
                    new { channel = channelId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<BaseResponse>(requestUrl, cancellationToken));
        }

        public IObservable<GroupResponse> Create(string name)
        {
            name.ThrowIfNullOrEmpty("name");

            return ObservableApiCall(CREATE_METHOD,
                    new { name = name },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<GroupResponse>(requestUrl, cancellationToken));
        }

        public IObservable<GroupResponse> CreateChild(string channelId)
        {
            channelId.ThrowIfNull("channelId");

            return ObservableApiCall(CREATE_CHILD_METHOD,
                    new { channel = channelId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<GroupResponse>(requestUrl, cancellationToken));
        }

        public IObservable<MessagesResponse> History(string channelId, double? latest = null,
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
                    async (requestUrl, cancellationToken) => await GetResponseAsync<MessagesResponse>(requestUrl, cancellationToken));
        }

        public IObservable<GroupResponse> Info(string channelId)
        {
            channelId.ThrowIfNullOrEmpty("channel");

            return ObservableApiCall(INFO_METHOD,
                    new { channel = channelId },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<GroupResponse>(requestUrl, cancellationToken));
        }
    }
}
