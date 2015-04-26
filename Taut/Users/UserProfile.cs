using Newtonsoft.Json;
using System;

namespace Taut.Users
{
    public class UserProfile
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        public string Email { get; set; }

        public string Skype { get; set; }

        public string Phone { get; set; }

        [JsonProperty("image_24")]
        public Uri Image24 { get; set; }

        [JsonProperty("image_32")]
        public Uri Image32 { get; set; }

        [JsonProperty("image_48")]
        public Uri Image48 { get; set; }

        [JsonProperty("image_72")]
        public Uri Image72 { get; set; }

        [JsonProperty("image_192")]
        public Uri Image192 { get; set; }
    }
}
