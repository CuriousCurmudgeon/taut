using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Teams
{
    public class Team
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("email_domain")]
        public string EmailDomain { get; set; }

        public string Domain { get; set; }

        [JsonProperty("msg_edit_window_mins")]
        public string MessageEditWindowMins { get; set; }

        [JsonProperty("over_storage_limit")]
        public bool IsOverStorageLimit { get; set; }

        // TODO: Add team preferences. Need to trace so I know what the data structure looks like.
    }
}
