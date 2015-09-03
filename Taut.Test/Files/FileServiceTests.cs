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
        private static FileListResponse OkFileListResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkBaseResponse = JsonLoader.LoadJson<BaseResponse>(@"Channels/Data/base.json");
            OkFileInfoResponse = JsonLoader.LoadJson<FileInfoResponse>(@"Files/Data/file_info.json");
            OkFileListResponse = JsonLoader.LoadJson<FileListResponse>(@"Files/Data/file_list.json");
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
        public async Task WhenCountDoesNotHaveValue_ThenInfoDoesNotIncludeCountInParams()
        {
            await TestParamNotIncludedInInfoAsync("count");
        }

        [TestMethod]
        public async Task WhenCountHasValue_ThenInfoIncludesLatestInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileInfoResponse,
                async service => await service.Info("123", count:100).ToTask(),
                "*files.info*count=100");
        }

        #endregion

        #region page

        [TestMethod]
        public async Task WhenPageDoesNotHaveValue_ThenInfoDoesNotIncludePageInParams()
        {
            await TestParamNotIncludedInInfoAsync("page");
        }

        [TestMethod]
        public async Task WhenPageHasValue_ThenInfoIncludesPageInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileInfoResponse,
                async service => await service.Info("123", page: 2).ToTask(),
                "*files.info*page=2");
        }

        #endregion

        private async Task TestParamNotIncludedInInfoAsync(string paramName)
        {
            await ShouldHaveCalledTestHelperAsync(OkFileInfoResponse,
                async service => await service.Info("123").ToTask(),
                "*files.info*");
            GetApiCallPathAndQuery().ShouldNotContain(paramName);
        }

        #endregion

        #region List

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenFileIdIsNull_ThenListThrowsException()
        {
            // Arrange
            var service = BuildFileService();

            // Act
            service.List(null);
        }

        [TestMethod]
        public async Task WhenFileIdHasValue_ThenListIncludesFileInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123").ToTask(),
                "*files.list*file=123");
        }

        #region user

        [TestMethod]
        public async Task WhenUserDoesNotHaveValue_ThenListDoesNotIncludeUserInParams()
        {
            await TestParamNotIncludedInListAsync("user");
        }

        [TestMethod]
        public async Task WhenUserHasValue_ThenListIncludesUserInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123", userId: "456").ToTask(),
                "*files.list*user=456*");
        }

        #endregion

        #region ts_from

        [TestMethod]
        public async Task WhenTimestampFromDoesNotHaveValue_ThenListDoesNotInclude_ts_from_InParams()
        {
            await TestParamNotIncludedInListAsync("ts_from");
        }

        [TestMethod]
        public async Task WhenTimestampFromHasValue_ThenListIncludes_ts_from_InParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123", timestampFrom: 100).ToTask(),
                "*files.list*ts_from=100*");
        }

        #endregion

        #region ts_to

        [TestMethod]
        public async Task WhenTimestampToDoesNotHaveValue_ThenListDoesNotInclude_ts_to_InParams()
        {
            await TestParamNotIncludedInListAsync("ts_to");
        }

        [TestMethod]
        public async Task WhenTimestampToHasValue_ThenListIncludes_ts_to_InParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123", timestampTo: 100).ToTask(),
                "*files.list*ts_to=100*");
        }

        #endregion

        #region types

        [TestMethod]
        public async Task WhenTypesDoesNotHaveValue_ThenListSetsTypesToAllInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123").ToTask(),
                "*files.list*types=all*");
        }

        [TestMethod]
        public async Task WhenTypesHasMultipleValues_ThenListIncludesMultipleValuesInParams()
        {
            // The order of the types will be the order they are defined in the enum.
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123", types: FileTypes.GDocs | FileTypes.Images).ToTask(),
                "*files.list*types=images%2Cgdocs*");
        }

        #endregion

        #region count

        [TestMethod]
        public async Task WhenCountDoesNotHaveValue_ThenListDoesNotIncludeCountInParams()
        {
            await TestParamNotIncludedInListAsync("count");
        }

        [TestMethod]
        public async Task WhenCountHasValue_ThenListIncludesCountInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123", count: 100).ToTask(),
                "*files.list*count=100*");
        }

        #endregion

        #region page

        [TestMethod]
        public async Task WhenPageDoesNotHaveValue_ThenListDoesNotIncludePageInParams()
        {
            await TestParamNotIncludedInListAsync("page");
        }

        [TestMethod]
        public async Task WhenPageHasValue_ThenListIncludesPageInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123", page: 2).ToTask(),
                "*files.list*page=2*");
        }

        #endregion

        private async Task TestParamNotIncludedInListAsync(string paramName)
        {
            await ShouldHaveCalledTestHelperAsync(OkFileListResponse,
                async service => await service.List("123").ToTask(),
                "*files.list*");
            GetApiCallPathAndQuery().ShouldNotContain(paramName);
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
