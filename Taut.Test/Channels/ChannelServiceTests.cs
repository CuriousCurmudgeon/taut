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
        private static ChannelResponse OkChannelResponse;
        private static BaseResponse OkChannelArchiveResponse;
        private static ChannelHistoryResponse OkChannelHistoryResponse;
        private static ChannelListResponse OkChannelListResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkChannelResponse = JsonLoader.LoadJson<ChannelResponse>(@"Channels/Data/channel_info.json");
            OkChannelArchiveResponse = JsonLoader.LoadJson<BaseResponse>(@"Channels/Data/channel_archive.json");
            OkChannelHistoryResponse = JsonLoader.LoadJson<ChannelHistoryResponse>(@"Channels/Data/channel_history.json");
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

        #region Create

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenNameIsNull_ThenCreateThrowsArgumentNullException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Create(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenNameIsEmpty_ThenCreateThrowsArgumentException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Create("");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenNameIsWhitespace_ThenCreateThrowsArgumentException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Create("   ");
        }

        [TestMethod]
        public async Task WhenNameHasNonWhitespaceValue_ThenCreateIncludesNameInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Create("test").ToTask(),
                "*channels.create*name=test");
        }

        #endregion

        #region History

        #region channel

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenHistoryThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.History(null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenHistoryIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelHistoryResponse,
                async service => await service.History("123").ToTask(),
                "*channels.history*channel=123");
        }

        #endregion

        #region latest

        [TestMethod]
        public async Task WhenLatestDoesNotHaveValue_ThenHistoryDoesNotIncludeLatestInParams()
        {
            await TestParamNotIncludedAsync("latest");
        }

        [TestMethod]
        public async Task WhenLatestHasValue_ThenHistoryIncludesLatestInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelHistoryResponse,
                async service => await service.History("123", latest: 123.45).ToTask(),
                "*channels.history*latest=123.45");
        }

        #endregion

        #region oldest

        [TestMethod]
        public async Task WhenOldestDoesNotHaveValue_ThenHistoryDoesNotIncludeOldestInParams()
        {
            await TestParamNotIncludedAsync("oldest");
        }

        [TestMethod]
        public async Task WhenOldestHasValue_ThenHistoryIncludesOldestInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelHistoryResponse,
                async service => await service.History("123", oldest: 123.45).ToTask(),
                "*channels.history*oldest=123.45");
        }

        #endregion

        #region inclusive

        [TestMethod]
        public async Task WhenIsInclusiveDoesNotHaveValue_ThenHistoryDoesNotIncludeInclusiveInParams()
        {
            await TestParamNotIncludedAsync("inclusive");
        }

        [TestMethod]
        public async Task WhenIsInclusiveIsFalse_ThenHistoryIncludesInclusiveInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelHistoryResponse,
                async service => await service.History("123", isInclusive: false).ToTask(),
                "*channels.history*inclusive=0");
        }

        [TestMethod]
        public async Task WhenIsInclusiveIsTrue_ThenHistoryIncludesInclusiveInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelHistoryResponse,
                async service => await service.History("123", isInclusive: true).ToTask(),
                "*channels.history*inclusive=1");
        }

        #endregion

        #region count

        [TestMethod]
        public async Task WhenCountDoesNotHaveValue_ThenHistoryDoesNotIncludeCountInParams()
        {
            await TestParamNotIncludedAsync("count");
        }

        [TestMethod]
        public async Task WhenCountHasValue_ThenHistoryIncludesCountInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelHistoryResponse,
                async service => await service.History("123", count: 50).ToTask(),
                "*channels.history*count=50");
        }

        #endregion

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
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Info("123").ToTask(),
                "*channels.info*channel=123");
        }

        #endregion

        #region Invite

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenInviteThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Invite(null, "456");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenUserIdIsNull_ThenInviteThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Invite("123", null);
        }

        [TestMethod]
        public async Task WhenRequiredParamsHaveValues_ThenInfoIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Invite("123", "456").ToTask(),
                "*channels.invite*channel=123");
        }

        [TestMethod]
        public async Task WhenRequiredParamsHaveValues_ThenInfoIncludesUserIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Invite("123", "456").ToTask(),
                "*channels.invite*user=456");
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

        #region Name

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenNameIsNull_ThenJoinThrowsArgumentNullException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Join(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenNameIsEmpty_ThenJoinThrowsArgumentException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Join("");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenNameIsWhitespace_ThenJoinThrowsArgumentException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Join("   ");
        }

        [TestMethod]
        public async Task WhenNameHasNonWhitespaceValue_ThenJoinIncludesNameInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Join("test").ToTask(),
                "*channels.join*name=test");
        }

        #endregion

        #region Test Helpers

        private ChannelService BuildChannelService()
        {
            return new ChannelService(UserCredentialService.Object);
        }

        private async Task TestParamNotIncludedAsync(string paramName)
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelHistoryResponse,
                async service => await service.History("123").ToTask(),
                "*channels.history*");
            GetApiCallPathAndQuery().ShouldNotContain(paramName);
        }

        #endregion
    }
}
