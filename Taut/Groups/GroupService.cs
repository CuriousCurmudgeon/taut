using System;
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

        public IObservable<GroupCreateResponse> Create(string name)
        {
            name.ThrowIfNullOrEmpty("name");

            return ObservableApiCall(CREATE_METHOD,
                    new { name = name },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<GroupCreateResponse>(requestUrl, cancellationToken));
        }
    }
}
