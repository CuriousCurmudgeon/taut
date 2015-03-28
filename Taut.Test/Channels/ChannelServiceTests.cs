using Flurl.Http.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Taut.Authorizations;
using Taut.Channels;

namespace Taut.Test.Channels
{
    [TestClass]
    public class ChannelServiceTests 
    {
        private const string CHANNEL_INFO_RESPONSE = "{\"ok\":true}";

        private HttpTest _httpTest;
        private Mock<IUserCredentialService> _userCredentialService;

        [TestInitialize]
        public void Initialize()
        {
            _httpTest = new HttpTest();
            _userCredentialService = new Mock<IUserCredentialService>(MockBehavior.Strict);
        }

        #region Info

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenInfoThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Info(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenInfoWithCancellationTokenThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Info(null, CancellationToken.None);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenInfoIncludesChannelIdInParams()
        {
            // Arrange
            var service = BuildChannelService();
            _httpTest.RespondWith(CHANNEL_INFO_RESPONSE);
            SetAuthorizedUserExpectations();

            // Act
            var infoObserver = await service.Info("123").ToTask();

            // Assert
            _httpTest.ShouldHaveCalled("*channels.info*channel=123")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        #endregion

        #region Test Helpers

        private ChannelService BuildChannelService()
        {
            return new ChannelService(_userCredentialService.Object);
        }

        private void SetAuthorizedUserExpectations(string accessToken = "secret")
        {
            _userCredentialService.Setup(x => x.IsAuthorized)
                .Returns(true);
            _userCredentialService.Setup(x => x.GetAuthorization())
                .Returns(new Authorization() { AccessToken = accessToken });
        }

        #endregion
    }
}
