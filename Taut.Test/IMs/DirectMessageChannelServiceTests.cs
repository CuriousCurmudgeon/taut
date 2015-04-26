using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Taut.IMs;

namespace Taut.Test.IMs
{
    [TestClass]
    public class DirectMessageChannelServiceTests : ApiServiceTestBase
    {
        private static DirectMessageChannelListResponse OkDirectMessageChannelListResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkDirectMessageChannelListResponse = JsonLoader.LoadJson<DirectMessageChannelListResponse>(@"IMs/Data/im_list.json");
        }

        #region List

        [TestMethod]
        public async Task WhenListIsCalled_ThenIMListIsIncludedInRequest()
        {
            await ShouldHaveCalledTestHelperAsync(OkDirectMessageChannelListResponse,
                async service => await service.List().ToTask(),
                "*im.list");
        }

        #endregion

        #region Helpers

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IDirectMessageChannelService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildDirectMessageChannelService();
            HttpTest.RespondWithJson(response);
            SetAuthorizedUserExpectations();

            // Act
            var result = await action.Invoke(service);

            // Assert
            HttpTest.ShouldHaveCalled(shouldHaveCalled)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        private DirectMessageChannelService BuildDirectMessageChannelService()
        {
            return new DirectMessageChannelService(UserCredentialService.Object);
        }

        #endregion
    }
}
