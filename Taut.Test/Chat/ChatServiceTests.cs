using Flurl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareApproach.TestingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Taut.Chat;

namespace Taut.Test.Chat
{
    [TestClass]
    public class ChatServiceTests : ApiServiceTestBase
    {
        private static ChatPostMessageResponse OkChatPostMessageResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkChatPostMessageResponse = JsonLoader.LoadJson<ChatPostMessageResponse>(@"Chat/Data/chat_post_message.json");
        }

        #region PostMessage

        #region channel

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenPostMessageThrowsException()
        {
            // Arrange
            var service = BuildChatService();

            // Act
            service.PostMessage(null, "test");
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenPostMessageIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*channel=123");
        }

        #endregion

        #region text

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenTextIsNull_ThenPostMessageThrowsException()
        {
            // Arrange
            var service = BuildChatService();

            // Act
            service.PostMessage("id", null);
        }

        [TestMethod]
        public async Task WhenTextHasValue_ThenPostMessageIncludesTextInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*text=test");
        }

        #endregion

        #region username

        [TestMethod]
        public async Task WhenUsernameDoesNotHaveValue_ThenPostMessageDoesNotIncludeUsernameInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*");
            GetApiCallPathAndQuery().ShouldNotContain("username");
        }

        [TestMethod]
        public async Task WhenUsernameHasValue_ThenPostMessageIncludesUsernameInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", username: "user").ToTask(),
                "*chat.postMessage*username=user");
        }

        #endregion

        #region as_user

        [TestMethod]
        public async Task WhenAsUserHasNoValue_ThenPostMessageDoesNotIncludeAsUserInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*");
            GetApiCallPathAndQuery().ShouldNotContain("as_user");
        }

        [TestMethod]
        public async Task WhenAsUserHasValue_ThenPostMessageIncludesAsUserInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", asUser: true).ToTask(),
                "*chat.postMessage*as_user=true");
        }

        #endregion

        #region parse

        [TestMethod]
        public async Task WhenParseIsDefault_ThenPostMessageDoesNotIncludeParseInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*");
            GetApiCallPathAndQuery().ShouldNotContain("parse");
        }

        [TestMethod]
        public async Task WhenParseIsFull_ThenPostMessageIncludesParseFullInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", parse: ParseMode.Full).ToTask(),
                "*chat.postMessage*parse=full");
        }

        [TestMethod]
        public async Task WhenParseIsNone_ThenPostMessageIncludesParseNoneInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", parse: ParseMode.None).ToTask(),
                "*chat.postMessage*parse=none");
        }

        #endregion

        #region link_names

        [TestMethod]
        public async Task WhenLinkNamesHasNoValue_ThenPostMessageDoesNotIncludeLinkNamesInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*");
            GetApiCallPathAndQuery().ShouldNotContain("link_names");
        }

        [TestMethod]
        public async Task WhenLinkNamesIsTrue_ThenPostMessageIncludesLinkNamesInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", linkNames: true).ToTask(),
                "*chat.postMessage*link_names=1");
        }

        [TestMethod]
        public async Task WhenLinkNamesIsFalse_ThenPostMessageIncludesLinkNamesInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", linkNames: false).ToTask(),
                "*chat.postMessage*link_names=0");
        }

        #endregion

        #region unfurl_links

        [TestMethod]
        public async Task WhenUnfurlLinksHasNoValue_ThenPostMessageDoesNotIncludeUnfurlLinksInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*");
            GetApiCallPathAndQuery().ShouldNotContain("unfurl_links");
        }

        [TestMethod]
        public async Task WhenUnfurlLinksIsTrue_ThenPostMessageIncludesUnfurlLinksInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", unfurlLinks: true).ToTask(),
                "*chat.postMessage*unfurl_links=true");
        }

        [TestMethod]
        public async Task WhenUnfurlLinksIsFalse_ThenPostMessageIncludesUnfurlLinksInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", unfurlLinks: false).ToTask(),
                "*chat.postMessage*unfurl_links=false");
        }

        #endregion

        #region unfurl_media

        [TestMethod]
        public async Task WhenUnfurlMediaHasNoValue_ThenPostMessageDoesNotIncludeUnfurlMediaInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*");
            GetApiCallPathAndQuery().ShouldNotContain("unfurl_media");
        }

        [TestMethod]
        public async Task WhenUnfurlMediaIsTrue_ThenPostMessageIncludesUnfurlMediaInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", unfurlMedia: true).ToTask(),
                "*chat.postMessage*unfurl_media=true");
        }

        [TestMethod]
        public async Task WhenUnfurlMediaIsFalse_ThenPostMessageIncludesUnfurlMediaInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", unfurlMedia: false).ToTask(),
                "*chat.postMessage*unfurl_media=false");
        }

        #endregion

        #region icon_url

        [TestMethod]
        public async Task WhenIconUrlHasNoValue_ThenPostMessageDoesNotIncludeIconUrlInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*");
            GetApiCallPathAndQuery().ShouldNotContain("icon_url");
        }

        [TestMethod]
        public async Task WhenIconUrlHasValue_ThenPostMessageIncludesIconUrlInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", iconUrl: new Url("https://api.slack.com")).ToTask(),
                "*chat.postMessage*icon_url=https%3A%2F%2Fapi.slack.com");
        }

        #endregion

        #region icon_emoji

        [TestMethod]
        public async Task WhenIconEmojiHasNoValue_ThenPostMessageDoesNotIncludeIconEmojiInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*");
            GetApiCallPathAndQuery().ShouldNotContain("icon_emoji");
        }

        [TestMethod]
        public async Task WhenIconEmojiHasValue_ThenPostMessageIncludesIconEmojiInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test", iconEmoji: ":test:").ToTask(),
                "*chat.postMessage*icon_emoji=%3Atest%3A");
        }

        #endregion

        #endregion

        #region Helpers

        /// <summary>
        /// Assumes that only one API call has been made and gets the path and query of that call.
        /// </summary>
        /// <returns></returns>
        private string GetApiCallPathAndQuery()
        {
            return HttpTest.CallLog.First().Request.RequestUri.PathAndQuery;
        }

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IChatService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildChatService();
            HttpTest.RespondWithJson(response);
            SetAuthorizedUserExpectations();

            // Act
            var result = await action.Invoke(service);

            // Assert
            HttpTest.ShouldHaveCalled(shouldHaveCalled)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        private ChatService BuildChatService()
        {
            return new ChatService(UserCredentialService.Object);
        }

        #endregion
    }
}
