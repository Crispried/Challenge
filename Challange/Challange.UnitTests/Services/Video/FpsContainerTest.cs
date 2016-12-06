using Challange.Domain.Entities;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class FpsContainerTest : TestCase
    {
        private IFpsContainer fpsContainer;

        private string pathToImage = @"bitmap\bitmap.jpg";

        [Test]
        public void GetKeys()
        {
            // Arrange
            fpsContainer = new FpsContainer();
            var test = new List<string> { "1", "2" };
            fpsContainer.InitializeFpses(test);
            // Act
            var propertyResult = fpsContainer.GetKeys;
            // Assert
            Assert.IsTrue(propertyResult == test);
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
            fpsContainer = new FpsContainer();
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
            fpsContainer = new FpsContainer();
            fpsContainer.InitializeFpses(keys);
            // Act
            var fps = fpsContainer.GetFpsByKey("1");
            var image = new System.Drawing.Bitmap(pathToImage);
            fps.AddFrame(image);
            // Assert
            Assert.IsTrue(fpsContainer.Fpses["1"].Frames[0] == image);
        }

        [Test]
        public void GetFpsByKeyReturnNullFps()
        {
            // Arrange
            List<string> keys = new List<string>()
            {
                "1", "2"
            };
            fpsContainer = new FpsContainer();
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
            fpsContainer = new FpsContainer();
            fpsContainer.InitializeFpses(keys);
            // Act
            fpsContainer.RemoveFpsByKey("2");
            // Assert
            Assert.IsFalse(fpsContainer.Fpses.ContainsKey("2"));
        }
    }
}
