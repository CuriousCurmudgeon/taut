using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Taut.RealTime;

namespace Taut.Test.RealTime
{
    [TestClass]
    public class RealTimeMessagingServiceTests : ApiServiceTestBase
    {
        private static RealTimeMessagingStartResponse OkRealTimeMessagingStartResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkRealTimeMessagingStartResponse = JsonLoader.LoadJson<RealTimeMessagingStartResponse>(@"RealTime/Data/rtm_start.json");
        }

        #region Start

        [TestMethod]
        public async Task WhenStartIsCalled_ThenRtmStartIsIncludedInRequest()
        {
            await ShouldHaveCalledTestHelperAsync(OkRealTimeMessagingStartResponse,
                async service => await service.Start().ToTask(),
                "*rtm.start");
        }

        #endregion

        #region Helpers

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IRealTimeMessagingService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildRealTimeMessagingService();
            HttpTest.RespondWithJson(response);
            SetAuthorizedUserExpectations();

            // Act
            var result = await action.Invoke(service);

            // Assert
            HttpTest.ShouldHaveCalled(shouldHaveCalled)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        #region Test Helpers

        private RealTimeMessagingService BuildRealTimeMessagingService()
        {
            return new RealTimeMessagingService(UserCredentialService.Object);
        }

        #endregion

        #endregion
    }
}
