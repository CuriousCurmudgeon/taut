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
        protected const string DEFAULT_ACCESS_TOKEN = "secret";

        [TestInitialize]
        public virtual void Initialize()
        {
            HttpTest = new HttpTest();
            UserCredentialService = new Mock<IUserCredentialService>(MockBehavior.Strict);
        }

        public HttpTest HttpTest { get; private set; }

        public Mock<IUserCredentialService> UserCredentialService { get; private set; }

        protected void SetAuthorizedUserExpectations(string accessToken = DEFAULT_ACCESS_TOKEN)
        {
            UserCredentialService.Setup(x => x.IsAuthorized)
                .Returns(true);
            UserCredentialService.SetupGet(x => x.Authorization)
                .Returns(new Authorization() { AccessToken = accessToken });
        }

        /// <summary>
        /// Assumes that only one API call has been made and gets the path and query of that call.
        /// </summary>
        /// <returns></returns>
        protected string GetApiCallPathAndQuery()
        {
            return HttpTest.CallLog.First().Request.RequestUri.PathAndQuery;
        }
    }
}
