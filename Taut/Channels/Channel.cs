using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Messages;
using Taut.Metadata;

namespace Taut.Channels
{
    public class Channel
    {
        public Channel()
        {
            Members = new List<string>();
        }

        public string Id { get; set; }

        /// <summary>
        /// Channel name without the leading #.
        /// </summary>
        public string Name { get; set; }

        [JsonProperty("is_channel")]
        public bool IsChannel { get; set; }

        /// <summary>
        /// Channel creation date as a Unix timestamp.
        /// </summary>
        public int Created { get; set; }

        /// <summary>
        /// Id of the user who created the channel.
        /// </summary>
        public string Creator { get; set; }

        [JsonProperty("is_archived")]
        public bool IsArchived { get; set; }

        /// <summary>
        /// Is this the "general" channel? This channel will include all
        /// regular team members and commonly be called "#general".
        /// </summary>
        [JsonProperty("is_general")]
        public bool IsGeneral { get; set; }

        /// <summary>
        /// Ids of users who are members of the channel.
        /// </summary>
        public IEnumerable<string> Members { get; set; }

        public TextMetadata Topic { get; set; }

        public TextMetadata Purpose { get; set; }

        /// <summary>
        /// Is the user who requested the Channel a member of it?
        /// </summary>
        [JsonProperty("is_member")]
        public bool IsMember { get; set; }

        /// <summary>
        /// The Unix timestamp of the last message the user
        /// who requested the channel has read.
        /// </summary>
        [JsonProperty("last_read")]
        public double LastRead { get; set; }

        /// <summary>
        /// The latest message in the channel.
        /// </summary>
        public Message Latest { get; set; }

        /// <summary>
        /// A count of messages that the calling user has yet to read.
        /// </summary>
        [JsonProperty("unread_count")]
        public int UnreadCount { get; set; }
    }
}
