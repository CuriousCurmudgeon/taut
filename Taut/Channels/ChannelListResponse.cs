using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Channels
{
    public class ChannelListResponse : BaseResponse
    {
        public ChannelListResponse()
        {
            Channels = new List<Channel>();
        }

        public IEnumerable<Channel> Channels { get; set; }
    }
}
