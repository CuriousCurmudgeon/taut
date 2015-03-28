using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut
{
    public interface IAuthenticatedService
    {
        IUserCredentialService UserCredentialService { get; }
    }
}
