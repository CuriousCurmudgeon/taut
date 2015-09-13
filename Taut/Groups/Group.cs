using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Messages;
using Taut.Metadata;

namespace Taut.Groups
{
    public class Group
    {
        public Group()
        {
            Members = new List<string>();
        }

        public string Id { get; set; }

        /// <summary>
        /// Group name without the leading #.
        /// </summary>
        public string Name { get; set; }

        [JsonProperty("is_group")]
        public bool IsGroup { get; set; }

        /// <summary>
        /// Group creation date as a Unix timestamp.
        /// </summary>
        public int Created { get; set; }

        /// <summary>
        /// Id of the user who created the group.
        /// </summary>
        public string Creator { get; set; }

        [JsonProperty("is_archived")]
        public bool IsArchived { get; set; }

        /// <summary>
        /// Ids of users who are members of the group.
        /// </summary>
        public IEnumerable<string> Members { get; set; }

        public TextMetadata Topic { get; set; }

        public TextMetadata Purpose { get; set; }

        /// <summary>
        /// The Unix timestamp of the last message the user
        /// who requested the group has read.
        /// </summary>
        [JsonProperty("last_read")]
        public double LastRead { get; set; }

        /// <summary>
        /// The latest message in the group.
        /// </summary>
        public Message Latest { get; set; }

        /// <summary>
        /// A count of messages that the calling user has yet to read.
        /// </summary>
        [JsonProperty("unread_count")]
        public int UnreadCount { get; set; }

        /// <summary>
        /// A count of messages that the calling user has yet to read
        /// that matters to them.
        /// </summary>
        [JsonProperty("unread_count_display")]
        public int UnreadCountDisplay { get; set; }

        /// <summary>
        /// See https://api.slack.com/methods/groups.createChild for details on how child groups work.
        /// </summary>
        [JsonProperty("parent_group")]
        public string ParentGroupId { get; set; }
    }
}
