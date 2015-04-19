﻿using Flurl;
using System;
using System.Collections.Generic;
using Taut.Authorizations;

namespace Taut.Chat
{
    public class ChatService : BaseAuthenticatedApiService, IChatService
    {
        private const string POST_MESSAGE_METHOD = "chat.postMessage";

        public ChatService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<ChatPostMessageResponse> PostMessage(string channelId, string text,
            string username = null, bool? asUser = null, ParseMode parse = ParseMode.Default,
            bool? linkNames = null, bool? unfurlLinks = null, bool? unfurlMedia = null,
            Url iconUrl = null, string iconEmoji = null)
        {
            channelId.ThrowIfNull("channelId");
            text.ThrowIfNull("text");

            var queryParams = new Dictionary<string, object>
            {
                { "channel", channelId },
                { "text", text },
                { "username", username },
                { "as_user", asUser.HasValue ? asUser.ToString().ToLower() : null },
                { "parse", parse == ParseMode.Default ? null : parse.ToString().ToLower() },
                { "unfurl_links", unfurlLinks.HasValue ? unfurlLinks.ToString().ToLower() : null },
                { "unfurl_media", unfurlMedia.HasValue ? unfurlMedia.ToString().ToLower() : null },
                { "icon_url", iconUrl != null ? iconUrl.ToString() : null },
                { "icon_emoji", iconEmoji },
            };

            if(linkNames.HasValue)
            {
                queryParams.Add("link_names", Convert.ToInt32(linkNames.Value));
            }

            return ObservableApiCall(POST_MESSAGE_METHOD, queryParams,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChatPostMessageResponse>(requestUrl, cancellationToken));
        }
    }
}
