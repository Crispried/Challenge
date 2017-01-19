using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;
using NUnit.Framework;
using System.Collections.Generic;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class FpsContainerTest : TestCase
    {
        private IFpsContainer fpsContainer;

        [SetUp]
        public void SetUp()
        {
            fpsContainer = new FpsContainer();
        }

        [Test]
        public void InitializeFpses()
        {
            // Arrange
            List<string> keys = new List<string>()
            {
                "1", "2", "3", "4", "5"
            };

            // Act
            fpsContainer.InitializeFpses(keys);
            // Assert
            Assert.IsNotNull(fpsContainer.Fpses);
            Assert.IsTrue(fpsContainer.Fpses.Count == keys.Count);
        }

        [Test]
        public void GetFpsByKeyReturnsFps()
        {
            // Arrange
            List<string> keys = new List<string>()
            {
                "1", "2"
            };
            fpsContainer.InitializeFpses(keys);
            // Act
            var fps = fpsContainer.GetFpsByKey("1");
            // Assert
            Assert.IsTrue(fps != null);
        }

        [Test]
        public void GetFpsByKeyReturnNullFps()
        {
            // Arrange
            List<string> keys = new List<string>()
            {
                "1", "2"
            };
            fpsContainer.InitializeFpses(keys);
            // Act
            var fps = fpsContainer.GetFpsByKey("3");
            // Assert
            Assert.IsTrue(fps.GetType() == typeof(NullFps));
        }

        [Test]
        public void RemoveFpsDoNothingIfFpsNotExists()
        {
            // Arrange
            List<string> keys = new List<string>()
            {
                "1", "2"
            };
            fpsContainer = new FpsContainer();
            fpsContainer.InitializeFpses(keys);
            // Act
            fpsContainer.RemoveFpsByKey("3");
            // Assert
            Assert.IsTrue(fpsContainer.Fpses.Count == keys.Count);
        }

        [Test]
        public void RemoveFpsRemovesFpsIfItExists()
        {
            // Arrange
            List<string> keys = new List<string>()
            {
                "1", "2"
            };
            fpsContainer.InitializeFpses(keys);
            // Act
            fpsContainer.RemoveFpsByKey("2");
            // Assert
            Assert.IsFalse(fpsContainer.Fpses.ContainsKey("2"));
        }
    }
}
