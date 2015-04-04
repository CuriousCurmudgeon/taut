using Newtonsoft.Json;
using Taut.Messages;

namespace Taut.Chat
{
    public class ChatPostMessageResponse : BaseResponse
    {
        [JsonProperty("ts")]
        public double Timestamp { get; set; }

        [JsonProperty("channel")]
        public string ChannelId { get; set; }

        public Message Message { get; set; }
    }
}
