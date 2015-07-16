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
    public class ChannelServiceTests : ApiServiceTestBase
    {
        private static BaseResponse OkChannelArchiveResponse;
        private static ChannelInfoResponse OkChannelInfoResponse;
        private static ChannelListResponse OkChannelListResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkChannelArchiveResponse = JsonLoader.LoadJson<BaseResponse>(@"Channels/Data/channel_archive.json");
            OkChannelInfoResponse = JsonLoader.LoadJson<ChannelInfoResponse>(@"Channels/Data/channel_info.json");
            OkChannelListResponse = JsonLoader.LoadJson<ChannelListResponse>(@"Channels/Data/channel_list.json");
        }

        #region Archive

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenArchiveThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Archive(null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenArchiveIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelArchiveResponse,
                async service => await service.Archive("123").ToTask(),
                "*channels.archive*channel=123");
        }

        #endregion

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
            await ShouldHaveCalledTestHelperAsync(OkChannelInfoResponse,
                async service => await service.Info("123").ToTask(),
                "*channels.info*channel=123");
        }

        #endregion

        #region List

        [TestMethod]
        public async Task WhenExcludeArchivedIsDefault_ThenListParamsSetExcludeArchivedTo0()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelListResponse,
                async service => await service.List().ToTask(),
                "*channels.list*exclude_archived=0");
        }

        [TestMethod]
        public async Task WhenExcludeArchivedIsFalse_ThenListParamsSetExcludeArchivedTo0()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelListResponse,
                async service => await service.List(excludeArchived: false).ToTask(),
                "*channels.list*exclude_archived=0");
        }

        [TestMethod]
        public async Task WhenExcludeArchivedIsTrue_ThenListParamsSetExcludeArchivedTo1()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelListResponse,
                async service => await service.List(excludeArchived: true).ToTask(),
                "*channels.list*exclude_archived=1");
        }

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IChannelService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildChannelService();
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

        #region Test Helpers

        private ChannelService BuildChannelService()
        {
            return new ChannelService(UserCredentialService.Object);
        }

        #endregion
    }
}
