using Flurl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SoftwareApproach.TestingExtensions;
using System;
using Taut.Authorizations;

namespace Taut.Test
{
    [TestClass]
    public class BaseAuthenticatedApiServiceTests
    {
        private Mock<IUserCredentialService> _userCredentialService;

        [TestInitialize]
        public void Initialize()
        {
            _userCredentialService = new Mock<IUserCredentialService>(MockBehavior.Strict);
        }

        #region Constructor

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenUserCredentialServiceIsNull_ThenConstructorThrowsArgumentNullException()
        {
            // Act
            var service = new StubAuthenticatedApiService(null);
        }

        #endregion

        #region BuildRequestUrl

        [TestMethod, ExpectedException(typeof(UserNotAuthenticatedException))]
        public void WhenUserIsNotAuthenticated_ThenBuildRequestUrlThrowsException()
        {
            // Arrange
            var service = new StubAuthenticatedApiService(_userCredentialService.Object);
            _userCredentialService.Setup(x => x.IsAuthorized)
                .Returns(false);

            // Act
            service.BuildRequestUrl("test");
        }

        [TestMethod]
        public void WhenUserIsAuthenticated_ThenAccessTokenIsAddedToUrl()
        {
            TestBuildRequestUrlAccessToken(service => service.BuildRequestUrl("test"));
        }

        [TestMethod]
        public void WhenUserIsAuthenticatedAndQueryParamsIsNull_ThenAccessTokenIsAddedToUrl()
        {
            TestBuildRequestUrlAccessToken(service => service.BuildRequestUrl("test", null));
        }

        private void TestBuildRequestUrlAccessToken(Func<BaseAuthenticatedApiService, Url> act)
        {
            // Arrange
            var accessToken = "secretToken";

            var service = new StubAuthenticatedApiService(_userCredentialService.Object);
            _userCredentialService.Setup(x => x.IsAuthorized)
                .Returns(true);
            _userCredentialService.Setup(x => x.GetAuthorization())
                .Returns(new Authorization() { AccessToken = accessToken });

            // Act
            var url = act.Invoke(service);

            // Assert
            url.QueryParams.ShouldHaveCountOf(1);
            url.QueryParams["token"].ShouldEqual(accessToken);
        }

        #endregion
    }

    internal class StubAuthenticatedApiService : BaseAuthenticatedApiService
    {
        public StubAuthenticatedApiService(IUserCredentialService userCredentialService)
            : base(userCredentialService) { }
    }
}
