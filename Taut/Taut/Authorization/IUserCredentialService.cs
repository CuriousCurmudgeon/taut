using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taut.Authorization
{
    public interface IUserCredentialService
    {
        bool IsAuthorized { get; }

        Authorization GetAuthorization();

        void AddAuthorization(Authorization authorization);

        void ClearAuthorization();
    }
}
