using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using Taut.Users;

namespace Taut.Test.Users
{
    [TestClass]
    public class UserServiceTests : ApiServiceTestBase
    {
        private static UserListResponse OkUserListResponse;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            OkUserListResponse = JsonLoader.LoadJson<UserListResponse>(@"IMs/Data/im_list.json");
        }

        #region List

        [TestMethod]
        public async Task WhenListIsCalled_ThenUsersListIsIncludedInRequest()
        {
            await ShouldHaveCalledTestHelperAsync(OkUserListResponse,
                async service => await service.List().ToTask(),
                "*users.list");
        }

        #endregion

        #region Helpers

        private async Task ShouldHaveCalledTestHelperAsync<T>(T response, Func<IUserService, Task<T>> action,
            string shouldHaveCalled)
        {
            // Arrange
            var service = BuildUserService();
            HttpTest.RespondWithJson(response);
            SetAuthorizedUserExpectations();

            // Act
            var result = await action.Invoke(service);

            // Assert
            HttpTest.ShouldHaveCalled(shouldHaveCalled)
                .WithVerb(HttpMethod.Get)
                .Times(1);
        }

        private UserService BuildUserService()
        {
            return new UserService(UserCredentialService.Object);
        }

        #endregion
    }
}
