using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Users
{
    public interface IUserService
    {
        IObservable<UserListResponse> List();
    }
}
