using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Taut.Emoji;

namespace Taut.Test.Emoji
{
    [TestClass]
    public class EmojiServiceTests : ApiServiceTestBase
    {
        private static EmojiListResponse OkEmojiListResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkEmojiListResponse = JsonLoader.LoadJson<EmojiListResponse>(@"Emoji/Data/emoji_list.json");
        }

        #region List

        [TestMethod]
        public async Task WhenListIsCalled_ThenEmojiListIsCalled()
        {
            await ShouldHaveCalledTestHelperAsync(OkEmojiListResponse,
                async service => await service.List().ToTask(),
                "*emoji.list*");
        }

        #endregion

        #region Helpers

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IEmojiService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildEmojiService();
            HttpTest.RespondWithJson(response);
            SetAuthorizedUserExpectations();

            // Act
            var result = await action.Invoke(service);

            // Assert
            HttpTest.ShouldHaveCalled(shouldHaveCalled)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        private EmojiService BuildEmojiService()
        {
            return new EmojiService(UserCredentialService.Object);
        }

        #endregion
    }
}
