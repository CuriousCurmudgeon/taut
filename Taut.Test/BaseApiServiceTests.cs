using Flurl;
using Flurl.Http;
using Flurl.Http.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Taut.Test
{
    [TestClass]
    public class BaseApiServiceTests
    {
        private const string STUB_OK_RESPONSE = "{\"ok\":true}";
        private const string STUB_ERROR_RESPONSE = "{\"ok\":false, \"error\":\"channel_not_found\"}";

        private static Url ValidTestUrl = new Url(BaseApiService.API_URL);
        private HttpTest _httpTest;

        [TestInitialize]
        public void Setup()
        {
            _httpTest = new HttpTest();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _httpTest.Dispose();
        }

        #region BuildRequestUrl

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GivenABaseApiService_WhenPathIsNull_ThenBuildRequestUrlThrowsException()
        {
            // Arrange
            var apiService = new StubApiService();

            // Act
            apiService.BuildRequestUrl(null);
        }

        [TestMethod]
        public void GivenABaseApiService_WhenPathHasValue_ThenItIsAppendedToUrl()
        {
            // Arrange
            var apiService = new StubApiService();

            // Act
            var url = apiService.BuildRequestUrl("test");

            // Assert
            url.Path.ShouldEqual("https://slack.com/api/test");
        }

        [TestMethod]
        public void GivenABaseApiService_WhenQueryParamsIsNull_ThenBuildRequestUrlAppendsNoParams()
        {
            // Arrange
            var apiService = new StubApiService();

            // Act
            var url = apiService.BuildRequestUrl("test", null);

            // Assert
            url.QueryParams.ShouldBeEmpty();
        }

        #endregion

        #region GetResponseAsync

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public async Task GivenABaseApiService_WhenUrlIsNull_ThenGetResponseAsyncThrowsException()
        {
            // Arrange
            var apiService = new StubApiService();

            // Act
            await apiService.GetResponseAsync<StubResponse>(null, CancellationToken.None);
        }

        [TestMethod]
        public async Task GivenABaseApiService_WhenUrlHasValue_ThenGetResponseAsyncDeserializesResponse()
        {
            // Arrange
            var apiService = new StubApiService();
            _httpTest.RespondWith(STUB_OK_RESPONSE);

            // Act
            var response = await apiService.GetResponseAsync<StubResponse>(ValidTestUrl, CancellationToken.None);

            // Assert
            response.Ok.ShouldBeTrue();
        }

        [TestMethod]
        public async Task GivenABaseApiService_WhenUrlHasValue_ThenGetResponseAsyncMakesGetRequest()
        {
            // Arrange
            var apiService = new StubApiService();
            _httpTest.RespondWith(STUB_OK_RESPONSE);

            // Act
            var response = await apiService.GetResponseAsync<StubResponse>(ValidTestUrl, CancellationToken.None);

            // Assert
            _httpTest.ShouldHaveCalled(BaseApiService.API_URL)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        [TestMethod, ExpectedException(typeof(SlackApiException))]
        public async Task GivenABaseApiService_WhenGetResponseAsyncIsNotOk_ThenSlackApiExceptionIsThrown()
        {
            // Arrange
            var apiService = new StubApiService();
            _httpTest.RespondWith(STUB_ERROR_RESPONSE);

            // Act
            var response = await apiService.GetResponseAsync<StubResponse>(ValidTestUrl, CancellationToken.None);
        }

        [TestMethod]
        public async Task GivenABaseApiService_WhenGetResponseAsyncIsNotOk_ThenSlackApiExceptionHasErrorSet()
        {
            // Arrange
            var apiService = new StubApiService();
            _httpTest.RespondWith(STUB_ERROR_RESPONSE);

            SlackApiException exception = null;
            try
            {
                // Act
                var response = await apiService.GetResponseAsync<StubResponse>(ValidTestUrl, CancellationToken.None);
            }
            catch (SlackApiException ex)
            {
                exception = ex;
            }

            // Assert
            exception.Error.ShouldEqual("channel_not_found");
        }

        /// <summary>
        /// This test only exists to document that we are using the built-in Flurl error handling.
        /// It's possible that we could have special handling for some exceptions, but I'm not
        /// sure what that should be yet.
        /// </summary>
        /// <returns></returns>
        [TestMethod, ExpectedException(typeof(FlurlHttpException))]
        public async Task GivenABaseApiService_WhenGetResponseAsyncReturns404Error_ThenFlurlHttpExceptionThrown()
        {
            // Arrange
            var apiService = new StubApiService();
            _httpTest.RespondWith(404, string.Empty);

            // Act
            var response = await apiService.GetResponseAsync<StubResponse>(ValidTestUrl, CancellationToken.None);
        }

        #endregion
    }

    internal class StubApiService : BaseApiService {}

    internal class StubResponse : BaseResponse {}
}
