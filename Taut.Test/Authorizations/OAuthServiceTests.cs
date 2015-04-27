using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Taut.Authorizations;

namespace Taut.Test.Authorizations
{
    [TestClass]
    public class OAuthServiceTests : ApiServiceTestBase
    {
        private const string DEFAULT_CLIENT_ID = "123";
        private const string DEFAULT_CLIENT_SECRET = "456";

        private static Authorization AuthorizationResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            AuthorizationResponse = JsonLoader.LoadJson<Authorization>(@"Authorizations/Data/oauth_access.json");
        }

        #region Constructor

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenClientIdIsNull_ThenConstructorThrowsException()
        {
            var service = new OAuthService(null, "secret");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenClientSecretIsNull_ThenConstructorThrowsException()
        {
            var service = new OAuthService("id", null);
        }

        #endregion

        #region BuildOAuthUri

        [TestMethod]
        public void WhenBuildOAuthUriIsCalled_ThenResultContainsClientId()
        {
            // Arrange
            var service = BuildOAuthService();

            // Act
            var uri = service.BuildOAuthUri("state", AuthScopes.Identify);

            // Assert
            uri.AbsoluteUri.ShouldContain(string.Format("client_id={0}", DEFAULT_CLIENT_ID));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenStateIsNull_ThenBuildOAuthUriThrowsException()
        {
            // Arrange
            var service = BuildOAuthService();

            // Act
            var uri = service.BuildOAuthUri(null, AuthScopes.Identify);
        }

        [TestMethod]
        public void WhenBuildOAuthUriIsCalled_ThenResultContainsState()
        {
            // Arrange
            var service = BuildOAuthService();
            var state = "state";

            // Act
            var uri = service.BuildOAuthUri(state, AuthScopes.Identify);

            // Assert
            uri.AbsoluteUri.ShouldContain(string.Format("state={0}", state));
        }

        [TestMethod]
        public void WhenBuildOAuthUriIsCalled_ThenResulIsOAuthAuthorize()
        {
            // Arrange
            var service = BuildOAuthService();
            var state = "state";

            // Act
            var uri = service.BuildOAuthUri(state, AuthScopes.Identify);

            // Assert
            uri.AbsoluteUri.ShouldContain("https://slack.com/oauth/authorize");
        }

        #region redirect_uri

        [TestMethod]
        public void WhenRedirectUriIsNull_ThenResultDoesNotContainRedirectUri()
        {
            // Arrange
            var service = BuildOAuthService();

            // Act
            var uri = service.BuildOAuthUri("state", AuthScopes.Identify, redirectUri: null);

            // Assert
            uri.AbsoluteUri.ShouldNotContain("redirect_uri=");
        }

        [TestMethod]
        public void WhenRedirectUriHasValue_ThenResultDoesContainRedirectUri()
        {
            // Arrange
            var service = BuildOAuthService();

            // Act
            var uri = service.BuildOAuthUri("state", AuthScopes.Identify, redirectUri: new Uri("https://github.com/CuriousCurmudgeon/taut"));

            // Assert
            uri.AbsoluteUri.ShouldContain("redirect_uri=https%3A%2F%2Fgithub.com%2FCuriousCurmudgeon%2Ftaut");
        }

        #endregion

        #region team

        [TestMethod]
        public void WhenTeamIdIsNull_ThenResultDoesNotContainTeamId()
        {
            // Arrange
            var service = BuildOAuthService();

            // Act
            var uri = service.BuildOAuthUri("state", AuthScopes.Identify, teamId: null);

            // Assert
            uri.AbsoluteUri.ShouldNotContain("team=");
        }

        [TestMethod]
        public void WhenTeamIdHasValue_ThenResultDoesContainTeamId()
        {
            // Arrange
            var service = BuildOAuthService();
            var teamId = "789";

            // Act
            var uri = service.BuildOAuthUri("state", AuthScopes.Identify, teamId: teamId);

            // Assert
            uri.AbsoluteUri.ShouldContain(string.Format("team={0}", teamId));
        }

        #endregion

        #region scope

        [TestMethod]
        public void WhenScopeHasOneFlag_ThenResultContainsOneValueForScope()
        {
            // Arrange
            var service = BuildOAuthService();

            // Act
            var uri = service.BuildOAuthUri("state", AuthScopes.Identify);

            // Assert
            uri.AbsoluteUri.ShouldContain("scope=identify");
        }

        [TestMethod]
        public void WhenScopeHasTwoFlags_ThenResultContainsTwoValuesForScope()
        {
            // Arrange
            var service = BuildOAuthService();

            // Act
            var uri = service.BuildOAuthUri("state", AuthScopes.Identify | AuthScopes.Read);

            // Assert
            uri.AbsoluteUri.ShouldContain("scope=identify%2Cread");
        }

        #endregion

        #endregion

        #region Access

        [TestMethod]
        public async Task WhenAccessIsCalled_ThenClientIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(AuthorizationResponse,
                async service => await service.Access("789").ToTask(),
                "*oauth.access*client_id=123");
        }

        [TestMethod]
        public async Task WhenAccessIsCalled_ThenClientSecretInParams()
        {
            await ShouldHaveCalledTestHelperAsync(AuthorizationResponse,
                async service => await service.Access("789").ToTask(),
                "*oauth.access*client_secret=456");
        }

        #region code

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenCodeIsNull_ThenAccessThrowsException()
        {
            // Arrange
            var service = BuildOAuthService();

            // Act
            service.Access(null);
        }

        [TestMethod]
        public async Task WhenCodeHasValue_ThenAccessIncludesCodeInParams()
        {
            await ShouldHaveCalledTestHelperAsync(AuthorizationResponse,
                async service => await service.Access("789").ToTask(),
                "*oauth.access*code=789");
        }

        #endregion

        #region redirect_uri

        [TestMethod]
        public async Task WhenRedirectUriIsNull_ThenAccessRequestDoesNotContainRedirectUri()
        {
            await ShouldHaveCalledTestHelperAsync(AuthorizationResponse,
                async service => await service.Access("789").ToTask(),
                "*oauth.access*");
            GetApiCallPathAndQuery().ShouldNotContain("redirect_uri");
        }

        [TestMethod]
        public async Task WhenRedirectUriHasValue_ThenAccessRequestDoesContainRedirectUri()
        {
            await ShouldHaveCalledTestHelperAsync(AuthorizationResponse,
                async service => await service.Access("789", redirectUri: new Uri("https://github.com/CuriousCurmudgeon/taut")).ToTask(),
                "*oauth.access*redirect_uri=https%3A%2F%2Fgithub.com%2FCuriousCurmudgeon%2Ftaut");
        }

        #endregion

        #endregion

        #region Helpers

        private OAuthService BuildOAuthService(string clientId = DEFAULT_CLIENT_ID,
            string clientSecret = DEFAULT_CLIENT_SECRET)
        {
            return new OAuthService(clientId, clientSecret);
        }

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IOAuthService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildOAuthService();
            HttpTest.RespondWithJson(response);
            SetAuthorizedUserExpectations();

            // Act
            var result = await action.Invoke(service);

            // Assert
            HttpTest.ShouldHaveCalled(shouldHaveCalled)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        #endregion
    }
}
