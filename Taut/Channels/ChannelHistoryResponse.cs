using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Messages;

namespace Taut.Channels
{
    public class ChannelHistoryResponse : BaseResponse
    {
        public ChannelHistoryResponse()
        {
            Messages = new List<Message>();
        }

        public double Latest { get; set; }

        public IList<Message> Messages { get; set; }

        public bool HasMore { get; set; }
    }
}
