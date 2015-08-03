using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Channels
{
    public class ChannelLeaveResponse : BaseResponse
    {
        [JsonProperty("not_in_channel")]
        public bool NotInChannel { get; set; }
    }
}
