using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Taut.Files;

namespace Taut.Test.Files
{
    [TestClass]
    public class FileServiceTests : ApiServiceTestBase
    {
        private static BaseResponse OkBaseResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkBaseResponse = JsonLoader.LoadJson<BaseResponse>(@"Channels/Data/base.json");
        }

        #region Delete

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenFileIdIsNull_ThenDeleteThrowsException()
        {
            // Arrange
            var service = BuildFileService();

            // Act
            service.Delete(null);
        }

        [TestMethod]
        public async Task WhenFileIdHasValue_ThenDeleteIncludesFileInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkBaseResponse,
                async service => await service.Delete("123").ToTask(),
                "*files.delete*file=123");
        }

        #endregion

        #region Helpers

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IFileService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildFileService();
            HttpTest.RespondWithJson(response);
            SetAuthorizedUserExpectations();

            // Act
            var result = await action.Invoke(service);

            // Assert
            HttpTest.ShouldHaveCalled(shouldHaveCalled)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        private FileService BuildFileService()
        {
            return new FileService(UserCredentialService.Object);
        }

        #endregion
    }
}
