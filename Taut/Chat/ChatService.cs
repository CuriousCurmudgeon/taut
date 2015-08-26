using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Taut.Authorizations;
using Taut.Messages;

namespace Taut.Chat
{
    public class ChatService : BaseAuthenticatedApiService, IChatService
    {
        private const string DELETE_METHOD = "chat.delete";
        private const string POST_MESSAGE_METHOD = "chat.postMessage";
        private const string UPDATE_METHOD = "chat.update";

        public ChatService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }

        public IObservable<ChatDeleteResponse> Delete(double timestamp, string channelId)
        {
            timestamp.ThrowIfZero("timestamp");
            channelId.ThrowIfNull("channelId");

            return ObservableApiCall(DELETE_METHOD,
                    new { ts = timestamp, channel = channelId},
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChatDeleteResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChatPostMessageResponse> PostMessage(string channelId, string text,
            string username = null, bool? asUser = null, ParseMode parse = ParseMode.Default,
            bool? linkNames = null, bool? unfurlLinks = null, bool? unfurlMedia = null,
            Uri iconUrl = null, string iconEmoji = null, IEnumerable<Attachment> attachments = null)
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

            if (attachments != null)
            {
                var settings = new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                };
                
                queryParams.Add("attachments", JsonConvert.SerializeObject(attachments, settings));
            }

            return ObservableApiCall(POST_MESSAGE_METHOD, queryParams,
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChatPostMessageResponse>(requestUrl, cancellationToken));
        }

        public IObservable<ChatUpdateResponse> Update(double timestamp, string channelId, string text)
        {
            timestamp.ThrowIfZero("timestamp");
            channelId.ThrowIfNull("channelId");
            text.ThrowIfNullOrEmpty("text");

            return ObservableApiCall(UPDATE_METHOD,
                    new { ts = timestamp, channel = channelId, text = text },
                    async (requestUrl, cancellationToken) => await GetResponseAsync<ChatUpdateResponse>(requestUrl, cancellationToken));
        }
    }
}
