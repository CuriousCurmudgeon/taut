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

        private IUserCredentialService _service;

        [TestInitialize]
        public void Initialize()
        {
            _service = new UserCredentialService();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _service.Authorization = null;
        }

        #region IsAuthorized

        [TestMethod]
        public void GivenAUserCredentialService_WhenTheVaultIsEmpty_ThenIsAuthorizedReturnsFalse()
        {
            Assert.IsFalse(_service.IsAuthorized);
        }

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSet_ThenIsAuthorizedReturnsTrue()
        {
            // Arrange
            _service.Authorization = BuildAuthorization();

            // Act/Assert
            Assert.IsTrue(_service.IsAuthorized);
        }

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSetToNull_ThenIsAuthorizedReturnsFalse()
        {
            // Arrange
            _service.Authorization = BuildAuthorization();
            _service.Authorization = null;

            // Act/Assert
            Assert.IsFalse(_service.IsAuthorized);
        }

        #endregion

        #region Authorization

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSetToNull_ThenAuthorizationReturnsNull()
        {
            // Arrange
            _service.Authorization = null;

            // Act/Assert
            var authorization = _service.Authorization;

            // Assert
            Assert.IsNull(authorization);
        }

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSet_ThenAuthorizationReturnsMatchingAccessToken()
        {
            // Arrange
            var accessToken = "token";
            _service.Authorization = BuildAuthorization(accessToken: accessToken);

            // Act/Assert
            var authorization = _service.Authorization;

            // Assert
            Assert.AreEqual(accessToken, authorization.AccessToken);
        }

        [TestMethod]
        public void GivenAUserCredentialService_WhenAuthorizationIsSet_ThenAuthorizationReturnsMatchingScope()
        {
            // Arrange
            var scope = "read,write";
            _service.Authorization = BuildAuthorization(scope: scope);

            // Act/Assert
            var authorization = _service.Authorization;

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
