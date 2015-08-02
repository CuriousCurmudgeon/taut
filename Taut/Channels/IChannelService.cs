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
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<BaseResponse> Archive(string channelId);

        /// <summary>
        /// <a href="https://api.slack.com/methods/channels.create">Documentation</a>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<ChannelCreateResponse> Create(string name);

        /// <summary>
        /// <a href="https://api.slack.com/methods/channels.history">Documentation</a>.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if channelId is null</exception>
        /// <returns></returns>
        IObservable<ChannelHistoryResponse> History(string channelId, double? latest = null,
            double? oldest = null, bool? isInclusive = null, int? count = null);

        /// <summary>
        /// <a href="https://api.slack.com/methods/channels.info">Documentation</a>.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<ChannelInfoResponse> Info(string channelId);

        /// <summary>
        /// <a href="https://api.slack.com/methods/channels.list">Documentation</a>.
        /// </summary>
        /// <returns></returns>
        IObservable<ChannelListResponse> List(bool excludeArchived = false);
    }
}
