using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.IMs
{
    public class DirectMessageChannelListResponse : BaseResponse
    {
        public DirectMessageChannelListResponse()
        {
            DirectMessageChannels = new List<DirectMessageChannel>();
        }

        [JsonProperty("ims")]
        public IEnumerable<DirectMessageChannel> DirectMessageChannels { get; set; }
    }
}
