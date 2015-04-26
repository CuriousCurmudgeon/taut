using Newtonsoft.Json;
using System.Collections.Generic;
using Taut.Channels;
using Taut.IMs;
using Taut.Teams;
using Taut.Users;

namespace Taut.RealTime
{
    public class RealTimeMessagingStartResponse : BaseResponse
    {
        public RealTimeMessagingStartResponse()
        {
            Users = new List<User>();
            DirectMessageChannels = new List<DirectMessageChannel>();
        }

        public string Url { get; set; }

        public User Self { get; set; }

        public Team Team { get; set; }

        public IEnumerable<User> Users { get; set; }

        public IEnumerable<Channel> Channels { get; set; }

        // TOOD: Add groups.

        [JsonProperty("ims")]
        public IEnumerable<DirectMessageChannel> DirectMessageChannels { get; set; }

        // TODO: Add bots.
    }
}
