using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Emoji
{
    public class EmojiListResponse : BaseResponse
    {
        public EmojiListResponse()
        {
            Emoji = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Emoji { get; set; }
    }
}
