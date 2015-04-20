using Newtonsoft.Json;

namespace Taut.Messages
{
    public class AttachmentField
    {
        public string Title { get; set; }

        public string Value { get; set; }

        [JsonProperty("short")]
        public bool IsShort { get; set; }
    }
}
