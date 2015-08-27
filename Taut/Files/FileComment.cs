using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Files
{
    public class FileComment
    {
        public string Id { get; set; }

        public double Timestamp { get; set; }

        [JsonProperty("user")]
        public string UserId { get; set; }

        public string Comment { get; set; }
    }
}
