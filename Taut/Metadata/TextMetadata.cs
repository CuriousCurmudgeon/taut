using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Metadata
{
    public class TextMetadata
    {
        public string Value { get; set; }

        /// <summary>
        /// The Id of the user who set the value.
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// The Unix timestamp of when the value was last set.
        /// </summary>
        [JsonProperty("last_set")]
        public string LastSet { get; set; }
    }
}
