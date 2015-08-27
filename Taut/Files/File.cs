using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Messages;

namespace Taut.Files
{
    public class File
    {
        public File()
        {
            Channels = new List<string>();
            Groups = new List<string>();
            IMs = new List<string>();
            PinnedTo = new List<string>();
            Reactions = new List<Reaction>();
        }

        public string Id { get; set; }

        public double Timestamp { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string MimeType { get; set; }

        public string FileType { get; set; }

        [JsonProperty("pretty_type")]
        public string PrettyType { get; set; }

        [JsonProperty("user")]
        public string UserId { get; set; }

        public string Mode { get; set; }

        public bool Editable { get; set; }

        public bool IsExternal { get; set; }

        [JsonProperty("external_type")]
        public string ExternalType { get; set; }

        public long Size { get; set; }

        public Uri Url { get; set; }

        [JsonProperty("url_download")]
        public Uri UrlDownload { get; set; }

        [JsonProperty("url_private")]
        public Uri UrlPrivate { get; set; }

        [JsonProperty("url_private_download")]
        public Uri UrlPrivateDownload { get; set; }

        [JsonProperty("thumb_64")]
        public Uri Thumb64 { get; set; }

        [JsonProperty("thumb_80")]
        public Uri Thumb80 { get; set; }

        [JsonProperty("thumb_360")]
        public Uri Thumb360 { get; set; }

        [JsonProperty("thumb_360_gif")]
        public Uri Thumb360Gif { get; set; }

        [JsonProperty("thumb_360_w")]
        public int Thumb360Width { get; set; }

        [JsonProperty("thumb_360_h")]
        public int Thumb360Height { get; set; }

        public Uri Permalink { get; set; }

        [JsonProperty("edit_link")]
        public Uri EditLink { get; set; }

        public string Preview { get; set; }

        [JsonProperty("preview_highlight")]
        public string PreviewHighlight { get; set; }

        public int Lines { get; set; }

        [JsonProperty("lines_more")]
        public int LinesMore { get; set; }

        public bool IsPublic { get; set; }

        [JsonProperty("public_url_shared")]
        public bool PublicUrlShared { get; set; }

        public IEnumerable<string> Channels { get; set; }

        public IEnumerable<string> Groups { get; set; }

        public IEnumerable<string> IMs { get; set; }

        [JsonProperty("initial_comment")]
        public FileComment InitialComment { get; set; }

        [JsonProperty("num_stars")]
        public int NumberOfStars { get; set; }

        [JsonProperty("is_starred")]
        public bool IsStarred { get; set; }

        public IEnumerable<string> PinnedTo { get; set; }

        public IEnumerable<Reaction> Reactions { get; set; }
    }
}
