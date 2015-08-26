using System;
using System.Collections.Generic;
using Taut.Messages;

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
        /// <summary>
        /// <a href="https://api.slack.com/methods/chat.delete">Documentation</a>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<ChatDeleteResponse> Delete(double timestamp, string channelId);

        /// <summary>
        /// <a href="https://api.slack.com/methods/chat.postMessage">Documentation</a>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<ChatPostMessageResponse> PostMessage(string channelId, string text,
            string username = null, bool? asUser = null, ParseMode parse = ParseMode.Default,
            bool? linkNames = null, bool? unfurlLinks = null, bool? unfurlMedia = null,
            Uri iconUrl = null, string iconEmoji = null, IEnumerable<Attachment> attachments = null);

        /// <summary>
        /// <a href="https://api.slack.com/methods/chat.update">Documentation</a>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<ChatUpdateResponse> Update(double timestamp, string channelId, string text);
    }
}
