using System;
using System.Threading;

namespace Taut.Channels
{
    public interface IChannelService : IAuthenticatedService
    {
        /// <summary>
        /// <a href="https://api.slack.com/methods/channels.info">Documentation</a>.
        /// </summary>
        /// <param name="channelId"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        IObservable<ChannelInfoResponse> Info(string channelId);
        IObservable<ChannelInfoResponse> Info(string channelId, CancellationToken cancellationToken);
    }
}
