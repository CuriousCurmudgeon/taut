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
using Taut.Groups;

namespace Taut.Test.Channels
{
    [TestClass]
    public class GroupServiceTests : ApiServiceTestBase
    {
        private static BaseResponse OkBaseResponse;
        private static GroupResponse OkGroupResponse;
        private static MessagesResponse OkMessagesResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkBaseResponse = JsonLoader.LoadJson<BaseResponse>(@"Data/base.json");
            OkGroupResponse = JsonLoader.LoadJson<GroupResponse>(@"Groups/Data/group_create.json");
            OkMessagesResponse = JsonLoader.LoadJson<MessagesResponse>(@"Data/messages.json");
        }

        #region Archive

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenArchiveThrowsException()
        {
            // Arrange
            var service = BuildGroupService();

            // Act
            service.Archive(null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenArchiveIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkBaseResponse,
                async service => await service.Archive("123").ToTask(),
                "*groups.archive*channel=123");
        }

        #endregion

        #region Close

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenCloseThrowsException()
        {
            // Arrange
            var service = BuildGroupService();

            // Act
            service.Close(null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenCloseIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkBaseResponse,
                async service => await service.Close("123").ToTask(),
                "*groups.close*channel=123");
        }

        #endregion

        #region Create

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenNameIsNull_ThenCreateThrowsArgumentNullException()
        {
            // Arrange
            var service = BuildGroupService();

            // Act
            service.Create(null);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenNameIsEmpty_ThenCreateThrowsArgumentException()
        {
            // Arrange
            var service = BuildGroupService();

            // Act
            service.Create("");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void WhenNameIsWhitespace_ThenCreateThrowsArgumentException()
        {
            // Arrange
            var service = BuildGroupService();

            // Act
            service.Create("   ");
        }

        [TestMethod]
        public async Task WhenNameHasNonWhitespaceValue_ThenCreateIncludesNameInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkGroupResponse,
                async service => await service.Create("test").ToTask(),
                "*groups.create*name=test");
        }

        #endregion

        #region CreateChild

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenCreateChildThrowsException()
        {
            // Arrange
            var service = BuildGroupService();

            // Act
            service.CreateChild(null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenCreateChildIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkGroupResponse,
                async service => await service.CreateChild("123").ToTask(),
                "*groups.createChild*channel=123");
        }

        #endregion

        #region History

        #region channel

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenHistoryThrowsException()
        {
            // Arrange
            var service = BuildGroupService();

            // Act
            service.History(null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenHistoryIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123").ToTask(),
                "*groups.history*channel=123");
        }

        #endregion

        #region latest

        [TestMethod]
        public async Task WhenLatestDoesNotHaveValue_ThenHistoryDoesNotIncludeLatestInParams()
        {
            await TestParamNotIncludedInGroupHistoryAsync("latest");
        }

        [TestMethod]
        public async Task WhenLatestHasValue_ThenHistoryIncludesLatestInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123", latest: 123.45).ToTask(),
                "*groups.history*latest=123.45");
        }

        #endregion

        #region oldest

        [TestMethod]
        public async Task WhenOldestDoesNotHaveValue_ThenHistoryDoesNotIncludeOldestInParams()
        {
            await TestParamNotIncludedInGroupHistoryAsync("oldest");
        }

        [TestMethod]
        public async Task WhenOldestHasValue_ThenHistoryIncludesOldestInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123", oldest: 123.45).ToTask(),
                "*groups.history*oldest=123.45");
        }

        #endregion

        #region inclusive

        [TestMethod]
        public async Task WhenIsInclusiveDoesNotHaveValue_ThenHistoryDoesNotIncludeInclusiveInParams()
        {
            await TestParamNotIncludedInGroupHistoryAsync("inclusive");
        }

        [TestMethod]
        public async Task WhenIsInclusiveIsFalse_ThenHistoryIncludesInclusiveInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123", isInclusive: false).ToTask(),
                "*groups.history*inclusive=0");
        }

        [TestMethod]
        public async Task WhenIsInclusiveIsTrue_ThenHistoryIncludesInclusiveInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123", isInclusive: true).ToTask(),
                "*groups.history*inclusive=1");
        }

        #endregion

        #region count

        [TestMethod]
        public async Task WhenCountDoesNotHaveValue_ThenHistoryDoesNotIncludeCountInParams()
        {
            await TestParamNotIncludedInGroupHistoryAsync("count");
        }

        [TestMethod]
        public async Task WhenCountHasValue_ThenHistoryIncludesCountInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123", count: 50).ToTask(),
                "*groups.history*count=50");
        }

        #endregion

        private async Task TestParamNotIncludedInGroupHistoryAsync(string paramName)
        {
            await ShouldHaveCalledTestHelperAsync(OkMessagesResponse,
                async service => await service.History("123").ToTask(),
                "*groups.history*");
            GetApiCallPathAndQuery().ShouldNotContain(paramName);
        }

        #endregion

        #region Test Helpers

        private GroupService BuildGroupService()
        {
            return new GroupService(UserCredentialService.Object);
        }

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IGroupService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildGroupService();
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
