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
        private static BaseResponse OkBaseResponse;
        private static MessagesResponse OkMessagesResponse;
        private static ChannelLeaveResponse OkChannelLeaveResponse;
        private static ChannelListResponse OkChannelListResponse;
        private static ChannelSetPurposeResponse OkChannelSetPurposeResponse;
        private static ChannelSetTopicResponse OkChannelSetTopicResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkChannelResponse = JsonLoader.LoadJson<ChannelResponse>(@"Channels/Data/channel_info.json");
            OkBaseResponse = JsonLoader.LoadJson<BaseResponse>(@"Data/base.json");
            OkMessagesResponse = JsonLoader.LoadJson<MessagesResponse>(@"Data/messages.json");
            OkChannelLeaveResponse = JsonLoader.LoadJson<ChannelLeaveResponse>(@"Channels/Data/channel_leave.json");
            OkChannelListResponse = JsonLoader.LoadJson<ChannelListResponse>(@"Channels/Data/channel_list.json");
            OkChannelSetPurposeResponse = JsonLoader.LoadJson<ChannelSetPurposeResponse>(@"Channels/Data/channel_setPurpose.json");
            OkChannelSetTopicResponse = JsonLoader.LoadJson<ChannelSetTopicResponse>(@"Channels/Data/channel_setTopic.json");
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
            await ShouldHaveCalledTestHelperAsync(OkBaseResponse,
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
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
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
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
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
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
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
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123", isInclusive: false).ToTask(),
                "*channels.history*inclusive=0");
        }

        [TestMethod]
        public async Task WhenIsInclusiveIsTrue_ThenHistoryIncludesInclusiveInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
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
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
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
        public async Task WhenRequiredParamsHaveValues_ThenInviteIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Invite("123", "456").ToTask(),
                "*channels.invite*channel=123");
        }

        [TestMethod]
        public async Task WhenRequiredParamsHaveValues_ThenInviteIncludesUserIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Invite("123", "456").ToTask(),
                "*channels.invite*user=456");
        }

        #endregion

        #region Join

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

        #region Kick

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenKickThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Kick(null, "456");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenUserIdIsNull_ThenKickThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Kick("123", null);
        }

        [TestMethod]
        public async Task WhenRequiredParamsHaveValues_ThenKickIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Kick("123", "456").ToTask(),
                "*channels.kick*channel=123");
        }

        [TestMethod]
        public async Task WhenRequiredParamsHaveValues_ThenKickIncludesUserIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Kick("123", "456").ToTask(),
                "*channels.kick*user=456");
        }

        #endregion

        #region Leave

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenLeaveThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Leave(null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenLeaveIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelLeaveResponse,
                async service => await service.Leave("123").ToTask(),
                "*channels.leave*channel=123");
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

        #region Mark

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenMarkThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Mark(null, DateTime.Now.ToUtcUnixTimestamp());
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenTimestampIsZero_ThenMarkThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Mark("123", 0);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenMarkIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkBaseResponse,
                async service => await service.Mark("123", 123.45).ToTask(),
                "*channels.mark*channel=123");
        }

        [TestMethod]
        public async Task WhenTimestampHasValue_ThenMarkIncludesTSInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkBaseResponse,
                async service => await service.Mark("123", 123.45).ToTask(),
                "*channels.mark*ts=123.45");
        }

        #endregion

        #region Rename

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenRenameThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Rename(null, "new_name");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenNameIsNull_ThenRenameThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Rename("123", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenNameIsEmpty_ThenRenameThrowsArgumentException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Rename("123", "");
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenRenameIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Rename("123", "new_name").ToTask(),
                "*channels.rename*channel=123");
        }

        [TestMethod]
        public async Task WhenNameHasValue_ThenRenameIncludesNameInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelResponse,
                async service => await service.Rename("123", "new_name").ToTask(),
                "*channels.rename*name=new_name");
        }

        #endregion

        #region SetPurpose

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenSetPurposeThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.SetPurpose(null, "purpose");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenPurposeIsNull_ThenSetPurposeThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.SetPurpose("123", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenPurposeIsEmpty_ThenSetPurposeThrowsArgumentException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.SetPurpose("123", "");
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenSetPurposeIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelSetPurposeResponse,
                async service => await service.SetPurpose("123", "purpose").ToTask(),
                "*channels.setPurpose*channel=123");
        }

        [TestMethod]
        public async Task WhenPurposeHasValue_ThenSetPurposeIncludesPurposeInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelSetPurposeResponse,
                async service => await service.SetPurpose("123", "purpose").ToTask(),
                "*channels.setPurpose*purpose=purpose");
        }

        #endregion

        #region SetTopic

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenSetTopicThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.SetTopic(null, "Topic");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenTopicIsNull_ThenSetTopicThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.SetTopic("123", null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenTopicIsEmpty_ThenSetTopicThrowsArgumentException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.SetTopic("123", "");
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenSetTopicIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelSetTopicResponse,
                async service => await service.SetTopic("123", "Topic").ToTask(),
                "*channels.setTopic*channel=123");
        }

        [TestMethod]
        public async Task WhenTopicHasValue_ThenSetTopicIncludesTopicInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChannelSetTopicResponse,
                async service => await service.SetTopic("123", "Topic").ToTask(),
                "*channels.setTopic*topic=Topic");
        }

        #endregion

        #region Unarchive

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenUnarchiveThrowsException()
        {
            // Arrange
            var service = BuildChannelService();

            // Act
            service.Unarchive(null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenUnarchiveIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkBaseResponse,
                async service => await service.Unarchive("123").ToTask(),
                "*channels.unarchive*channel=123");
        }

        #endregion

        #region Test Helpers

        private ChannelService BuildChannelService()
        {
            return new ChannelService(UserCredentialService.Object);
        }

        private async Task TestParamNotIncludedAsync(string paramName)
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123").ToTask(),
                "*channels.history*");
            GetApiCallPathAndQuery().ShouldNotContain(paramName);
        }

        #endregion
    }
}
