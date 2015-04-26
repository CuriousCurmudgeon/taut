using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Messages;

namespace Taut.IMs
{
    public class DirectMessageChannel
    {
        public string Id { get; set; }

        [JsonProperty("is_im")]
        public bool IsDirectMessageChannel { get; set; }

        [JsonProperty("user")]
        public string UserId { get; set; }

        public double Created { get; set; }

        [JsonProperty("is_user_deleted")]
        public bool IsUserDeleted { get; set; }

        [JsonProperty("is_open")]
        public bool IsOpen { get; set; }

        [JsonProperty("last_read")]
        public double LastRead { get; set; }

        [JsonProperty("unread_count")]
        public int UnreadCount { get; set; }

        public Message Latest { get; set; }
    }
}
