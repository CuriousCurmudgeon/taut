using Flurl.Http.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.Test
{
    public class ApiServiceTestBase
    {
        [TestInitialize]
        public virtual void Initialize()
        {
            HttpTest = new HttpTest();
            UserCredentialService = new Mock<IUserCredentialService>(MockBehavior.Strict);
        }

        public HttpTest HttpTest { get; private set; }

        public Mock<IUserCredentialService> UserCredentialService { get; private set; }
    }
}
