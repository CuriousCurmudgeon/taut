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

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkBaseResponse = JsonLoader.LoadJson<BaseResponse>(@"Channels/Data/base.json");
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
