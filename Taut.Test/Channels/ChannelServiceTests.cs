using Flurl.Http.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SoftwareApproach.TestingExtensions;
using System;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
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
        private static ChannelInfoResponse OkChannelInfoResponse;

        private HttpTest _httpTest;
        private Mock<IUserCredentialService> _userCredentialService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkChannelInfoResponse = JsonLoader.LoadJson<ChannelInfoResponse>(@"Channels/Data/channel_info.json");
        }

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

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenInfoIncludesChannelIdInParams()
        {
            // Arrange
            var service = BuildChannelService();
            _httpTest.RespondWithJson(OkChannelInfoResponse);
            SetAuthorizedUserExpectations();

            // Act
            var infoObserver = await service.Info("123").ToTask();

            // Assert
            _httpTest.ShouldHaveCalled("*channels.info*channel=123")
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [TestMethod]
        public async Task WhenResponseIsOk_ThenResponseIncludesChannel()
        {
            // Arrange
            var service = BuildChannelService();
            _httpTest.RespondWithJson(OkChannelInfoResponse);
            SetAuthorizedUserExpectations();

            // Act
            var infoObserver = await service.Info("123").ToTask();

            // Assert
            infoObserver.Channel.ShouldNotBeNull();
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
