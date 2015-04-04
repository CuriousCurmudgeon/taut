using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;
using Taut.WinRT.Authorizations;

namespace Taut.WinRT.Test.Authorizations
{
    [TestClass]
    public class UserCredentialServiceTests
    {
        private const string DEFAULT_ACCESS_TOKEN = "123";
        private const string DEFAULT_SCOPE = "read";

        #region IsAuthorized

        [TestMethod]
        public void GivenAUserCredentialService_WhenTheVaultIsEmpty_ThenIsAuthorizedReturnsFalse()
        {
            // Arrange
            var service = new UserCredentialService();

            // Act/Assert
            Assert.IsFalse(service.IsAuthorized);
        }

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSet_ThenIsAuthorizedReturnsTrue()
        {
            // Arrange
            var service = new UserCredentialService();
            service.Authorization = BuildAuthorization();

            // Act/Assert
            Assert.IsTrue(service.IsAuthorized);
        }

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSetToNull_ThenIsAuthorizedReturnsFalse()
        {
            // Arrange
            var service = new UserCredentialService();
            service.Authorization = BuildAuthorization();
            service.Authorization = null;

            // Act/Assert
            Assert.IsFalse(service.IsAuthorized);
        }

        #endregion

        #region Authorization

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSetToNull_ThenAuthorizationReturnsNull()
        {
            // Arrange
            var service = new UserCredentialService();
            service.Authorization = null;

            // Act/Assert
            var authorization = service.Authorization;

            // Assert
            Assert.IsNull(authorization);
        }

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSet_ThenAuthorizationReturnsMatchingAccessToken()
        {
            // Arrange
            var accessToken = "token";
            var service = new UserCredentialService();
            service.Authorization = BuildAuthorization(accessToken: accessToken);

            // Act/Assert
            var authorization = service.Authorization;

            // Assert
            Assert.AreEqual(accessToken, authorization.AccessToken);
        }

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSet_ThenAuthorizationReturnsMatchingScope()
        {
            // Arrange
            var scope = "read,write";
            var service = new UserCredentialService();
            service.Authorization = BuildAuthorization(scope: scope);

            // Act/Assert
            var authorization = service.Authorization;

            // Assert
            Assert.AreEqual(scope, authorization.Scope);
        }

        #endregion

        #region Helpers

        private Authorization BuildAuthorization(string accessToken = DEFAULT_ACCESS_TOKEN,
            string scope = DEFAULT_SCOPE)
        {
            return new Authorization
            {
                AccessToken = accessToken,
                Scope = scope,
            };
        }

        #endregion
    }
}
