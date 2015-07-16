using System;
using System.Collections.Generic;
using System.Threading;

namespace Taut.Channels
{
    public interface IChannelService : IAuthenticatedService
    {
        /// <summary>
        /// <a href="https://api.slack.com/methods/channels.archive">Documentation</a>.
        /// </summary>
        /// <param name="channelId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<BaseResponse> Archive(string channelId);

        /// <summary>
        /// <a href="https://api.slack.com/methods/channels.info">Documentation</a>.
        /// </summary>
        /// <param name="channelId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<ChannelInfoResponse> Info(string channelId);

        /// <summary>
        /// <a href="https://api.slack.com/methods/channels.list">Documentation</a>.
        /// </summary>
        /// <param name="excludeArchived">Should archived channels be returned?</param>
        /// <returns></returns>
        IObservable<ChannelListResponse> List(bool excludeArchived = false);
    }
}
