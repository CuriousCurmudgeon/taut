using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.MetaData;

namespace Taut.Messages
{
    public class Message
    {
        public string Type { get; set; }

        public string Subtype { get; set; }

        [JsonProperty("hidden")]
        public bool IsHidden { get; set; }

        [JsonProperty("channel")]
        public string ChannelId { get; set; }

        [JsonProperty("user")]
        public string UserId { get; set; }

        public string Text { get; set; }

        [JsonProperty("ts")]
        public double Timestamp { get; set; }

        [JsonProperty("deleted_ts")]
        public double DeletedTimestamp { get; set; }

        [JsonProperty("event_ts")]
        public double EventTimestamp { get; set; }

        public EditMetadata Edited { get; set; }
    }
}
