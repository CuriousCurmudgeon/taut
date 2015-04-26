using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.IMs
{
    public interface IDirectMessageChannelService
    {
        IObservable<DirectMessageChannelListResponse> List();
    }
}
