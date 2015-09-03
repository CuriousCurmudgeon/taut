using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
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
        private static FileInfoResponse OkFileInfoResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkBaseResponse = JsonLoader.LoadJson<BaseResponse>(@"Channels/Data/base.json");
            OkFileInfoResponse = JsonLoader.LoadJson<FileInfoResponse>(@"Files/Data/file_info.json");
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

        #region Info

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenFileIdIsNull_ThenInfoThrowsException()
        {
            // Arrange
            var service = BuildFileService();

            // Act
            service.Info(null);
        }

        [TestMethod]
        public async Task WhenFileIdHasValue_ThenInfoIncludesFileInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileInfoResponse,
                async service => await service.Info("123").ToTask(),
                "*files.info*file=123");
        }

        #region count

        [TestMethod]
        public async Task WhenCountDoesNotHaveValue_ThenHistoryDoesNotIncludeCountInParams()
        {
            await TestParamNotIncludedAsync("count");
        }

        [TestMethod]
        public async Task WhenLatestHasValue_ThenHistoryIncludesLatestInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileInfoResponse,
                async service => await service.Info("123", count:100).ToTask(),
                "*files.info*count=100");
        }

        #endregion

        #region page

        [TestMethod]
        public async Task WhenPageDoesNotHaveValue_ThenHistoryDoesNotIncludePageInParams()
        {
            await TestParamNotIncludedAsync("page");
        }

        [TestMethod]
        public async Task WhenPageHasValue_ThenHistoryIncludesPageInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileInfoResponse,
                async service => await service.Info("123", page: 2).ToTask(),
                "*files.info*page=2");
        }

        #endregion

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

        private async Task TestParamNotIncludedAsync(string paramName)
        {
            await ShouldHaveCalledTestHelperAsync(OkFileInfoResponse,
                async service => await service.Info("123").ToTask(),
                "*files.info*");
            GetApiCallPathAndQuery().ShouldNotContain(paramName);
        }

        private FileService BuildFileService()
        {
            return new FileService(UserCredentialService.Object);
        }

        #endregion
    }
}
