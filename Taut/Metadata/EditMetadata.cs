using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.MetaData
{
    public class EditMetadata
    {
        [JsonProperty("user")]
        public string UserId { get; set; }

        [JsonProperty("ts")]
        public double Timestamp { get; set; }
    }
}
