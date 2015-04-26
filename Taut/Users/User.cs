using Newtonsoft.Json;

namespace Taut.Users
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("deleted")]
        public bool IsDeleted { get; set; }

        public string Color { get; set; }

        public UserProfile Profile { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("is_owner")]
        public bool IsOwner { get; set; }

        [JsonProperty("is_primary_owner")]
        public bool IsPrimaryOwner { get; set; }

        [JsonProperty("is_restricted")]
        public bool IsRestricted { get; set; }

        [JsonProperty("is_ultra_restricted")]
        public bool IsUltraRestricted { get; set; }

        [JsonProperty("has_files")]
        public bool HasFiles { get; set; }

        [JsonProperty("manual_response")]
        public string ManualPresence { get; set; }
    }
}
