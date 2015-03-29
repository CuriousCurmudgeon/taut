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
        private static ChannelInfoResponse OkChannelInfoResponse;
        private static ChannelListResponse OkChannelListResponse;

        private HttpTest _httpTest;
        private Mock<IUserCredentialService> _userCredentialService;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkChannelInfoResponse = JsonLoader.LoadJson<ChannelInfoResponse>(@"Channels/Data/channel_info.json");
            OkChannelListResponse = JsonLoader.LoadJson<ChannelListResponse>(@"Channels/Data/channel_list.json");
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
            await ApiCallTestHelperAsync(OkChannelInfoResponse,
                async service => await service.Info("123").ToTask(),
                "*channels.info*channel=123");
        }

        #endregion

        #region List

        [TestMethod]
        public async Task WhenExcludeArchivedIsDefault_ThenListParamsSetExcludeArchivedTo0()
        {
            await ApiCallTestHelperAsync(OkChannelListResponse,
                async service => await service.List().ToTask(),
                "*channels.list*exclude_archived=0");
        }

        [TestMethod]
        public async Task WhenExcludeArchivedIsFalse_ThenListParamsSetExcludeArchivedTo0()
        {
            await ApiCallTestHelperAsync(OkChannelListResponse,
                async service => await service.List(excludeArchived: false).ToTask(),
                "*channels.list*exclude_archived=0");
        }

        [TestMethod]
        public async Task WhenExcludeArchivedIsTrue_ThenListParamsSetExcludeArchivedTo1()
        {
            await ApiCallTestHelperAsync(OkChannelListResponse,
                async service => await service.List(excludeArchived: true).ToTask(),
                "*channels.list*exclude_archived=1");
        }

        private async Task ApiCallTestHelperAsync<T>(T response, Func<IChannelService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildChannelService();
            _httpTest.RespondWithJson(response);
            SetAuthorizedUserExpectations();

            // Act
            var result = await action.Invoke(service);

            // Assert
            _httpTest.ShouldHaveCalled(shouldHaveCalled)
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
