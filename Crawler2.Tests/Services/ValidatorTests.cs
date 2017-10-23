using NUnit.Framework;
using Crawler2.BLL.Services;

namespace Crawler2.Tests.Services
{
    [TestFixture]
    class ValidatorTests
    {
        [Test]
        public void CheckDeep_PassCorrectParameter_ShouldReturnTrue()
        {
            // Arrange
            var validator = new Validator();

            // Act
            var result = validator.CheckDeep(3);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.IsNull(result.Message);
        }

        [Test]
        public void CheckDeep_PassIncorrectParameter_ShouldReturnFalse()
        {
            // Arrange
            var validator = new Validator();

            // Act
            var result = validator.CheckDeep(-1);

            // Assert
            Assert.AreEqual(false, result.Success);
            Assert.IsNotNull(result.Message);
        }

        [Test]
        public void CheckWord_PassEmptyString_ShouldReturnFalse()
        {
            // Arrange
            var validator = new Validator();

            // Act
            var result = validator.CheckWord("");

            // Assert
            Assert.AreEqual(false, result.Success);
            Assert.IsNotNull(result.Message);
        }

        [Test]
        public void CheckTimeLimit_PassCorrectTimeout_ShouldReturnTrue()
        {
            // Arrange
            var validator = new Validator();

            // Act
            var result = validator.CheckTimeLimit(100);

            // Assert
            Assert.AreEqual(true, result.Success);
            Assert.IsNull(result.Message);
        }

        [Test]
        public void CheckTimeLimit_PassIncorrectTimeout_ShouldReturnFalse()
        {
            // Arrange
            var validator = new Validator();

            // Act
            var result = validator.CheckTimeLimit(1000);

            // Assert
            Assert.AreEqual(false, result.Success);
            Assert.IsNotNull(result.Message);
        }

        [Test]
        public void CheckGroupSize_PassIncorrectSize_ShouldReturnFalse()
        {
            // Arrange
            var validator = new Validator();

            // Act
            var result = validator.CheckTimeLimit(-1);

            // Assert
            Assert.AreEqual(false, result.Success);
            Assert.IsNotNull(result.Message);
        }
    }
}
