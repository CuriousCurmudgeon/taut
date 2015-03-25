using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareApproach.TestingExtensions;

namespace Taut.Test
{
    [TestClass]
    public class BaseApiServiceTests
    {
        #region BuildRequestUrl

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenMethodIsNull_ThenBuildRequestUrlThrowsException()
        {
            // Arrange
            var apiService = new StubApiService();

            // Act
            apiService.BuildRequestUrl(null);
        }

        #endregion
    }

    internal class StubApiService : BaseApiService {}
}
