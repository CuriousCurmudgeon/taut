using Flurl;
using System;

namespace Taut.Chat
{
    /// <summary>
    /// The parsing mode for messages.
    /// See <see cref="https://api.slack.com/docs/formatting"/> for details.
    /// </summary>
    public enum ParseMode
    {
        Default,
        None,
        Full
    }

    public interface IChatService
    {
        IObservable<ChatPostMessageResponse> PostMessage(string channelId, string text,
            string username = null, bool? asUser = null, ParseMode parse = ParseMode.Default,
            bool? linkNames = null, bool? unfurlLinks = null, bool? unfurlMedia = null,
            Url iconUrl = null, string iconEmoji = null);
    }
}
