using System;
using Taut.Authorizations;

namespace Taut.Chat
{
    public class ChatService : BaseAuthenticatedApiService, IChatService
    {
        private const string POST_MESSAGE_METHOD = "chat.postMessage";

        public ChatService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<ChatPostMessageResponse> PostMessage(string channelId, string text,
            string username = null, bool? asUser = null)
        {
            channelId.ThrowIfNull("channelId");
            text.ThrowIfNull("text");

            var queryParams = new
            {
                channel = channelId,
                text = text,
                username = username,
                as_user = asUser.HasValue ? asUser.ToString().ToLower() : null,
            };

            return ObservableApiCall(POST_MESSAGE_METHOD, queryParams,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChatPostMessageResponse>(requestUrl, cancellationToken));
        }
    }
}
