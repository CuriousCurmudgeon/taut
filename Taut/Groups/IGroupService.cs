using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Groups
{
    public interface IGroupService
    {
        IObservable<BaseResponse> Archive(string channelId);
    }
}
