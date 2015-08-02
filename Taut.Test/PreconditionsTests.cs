using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taut.Test
{
    [TestClass]
    public class PreconditionsTests
    {
        #region ThrowIfNull

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenObjectIsNull_ThenThrowIfNullThrowsArgumentNullException()
        {
            // Arrange
            object obj = null;

            // Act/Assert
            obj.ThrowIfNull("obj");
        }

        [TestMethod]
        public void WhenObjectIsNotNull_ThenThrowIfNullDoesNotThrowException()
        {
            // Arrange
            object obj = new object();

            // Act/Assert
            obj.ThrowIfNull("obj");
        }

        #endregion

        #region ThrowIfNullOrEmpty

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WhenStringIsNull_ThenThrowIfNullOrEmptyThrowsArgumentNullException()
        {
            // Arrange
            string str = null;

            // Act/Assert
            str.ThrowIfNullOrEmpty("str");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenStringIsEmpty_ThenThrowIfNullOrEmptyThrowsArgumentException()
        {
            // Arrange
            string str = "";

            // Act/Assert
            str.ThrowIfNullOrEmpty("str");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void WhenStringIsWhitespace_ThenThrowIfNullOrEmptyThrowsArgumentException()
        {
            // Arrange
            string str = "   ";

            // Act/Assert
            str.ThrowIfNullOrEmpty("str");
        }

        [TestMethod]
        public void WhenStringHasNonWhitespaceValue_ThenThrowIfNullOrEmptyDoesNotThrowException()
        {
            // Arrange
            string str = "test";

            // Act/Assert
            str.ThrowIfNullOrEmpty("str");
        }

        #endregion
    }
}
