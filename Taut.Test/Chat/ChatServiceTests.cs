using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenChannelIdIsNull_ThenPostMessageThrowsException()
        {
            // Arrange
            var service = BuildChatService();

            // Act
            service.PostMessage(null, "test");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void WhenTextIsNull_ThenPostMessageThrowsException()
        {
            // Arrange
            var service = BuildChatService();

            // Act
            service.PostMessage("id", null);
        }

        [TestMethod]
        public async Task WhenChannelIdHasValue_ThenPostMessageIncludesChannelIdInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*channel=123");
        }

        [TestMethod]
        public async Task WhenTextHasValue_ThenPostMessageIncludesTextInParams()
        {
            await ShouldHaveCalledTestHelperAsync(OkChatPostMessageResponse,
                async service => await service.PostMessage("123", "test").ToTask(),
                "*chat.postMessage*text=test");
        }

        #endregion

        #region Helpers

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
