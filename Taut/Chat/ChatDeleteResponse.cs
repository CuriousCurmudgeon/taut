using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Chat
{
    public class ChatDeleteResponse : BaseResponse
    {
        [JsonProperty("ts")]
        public double Timestamp { get; set; }

        [JsonProperty("channel")]
        public string ChannelId { get; set; }
    }
}
