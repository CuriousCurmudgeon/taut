using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Taut.Messages
{
    /// <summary>
    /// <see cref="https://api.slack.com/docs/attachments"/>
    /// </summary>
    public class Attachment
    {
        public Attachment()
        {
            Fields = new List<AttachmentField>();
        }

        public string Fallback { get; set; }

        /// <summary>
        /// Hex definition or "good", "warning", or "danger"
        /// </summary>
        public string Color { get; set; }

        public string Pretext { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_link")]
        public Uri AuthorLink { get; set; }

        [JsonProperty("author_icon")]
        public Uri AuthorIcon { get; set; }

        public string Title { get; set; }

        [JsonProperty("title_link")]
        public Uri TitleLink { get; set; }

        public string Text { get; set; }

        [JsonProperty("image_url")]
        public Uri ImageUrl { get; set; }

        public IEnumerable<AttachmentField> Fields { get; set; }
    }
}
