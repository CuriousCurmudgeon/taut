using System;
using Taut.Authorizations;

namespace Taut.Chat
{
    public class ChatService : BaseAuthenticatedApiService, IChatService
    {
        private const string POST_MESSAGE_METHOD = "chat.postMessage";

        public ChatService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<ChatPostMessageResponse> PostMessage(string channelId, string text)
        {
            channelId.ThrowIfNull("channelId");
            text.ThrowIfNull("text");

            return ObservableApiCall(POST_MESSAGE_METHOD,
                    new { channel = channelId, text = text },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChatPostMessageResponse>(requestUrl, cancellationToken));
        }
    }
}
