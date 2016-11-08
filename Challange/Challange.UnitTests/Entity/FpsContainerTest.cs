using Challange.Domain.Entities;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Servuces.Video.Concrete;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class FpsContainerTest
    {
        private IFpsContainer fpsContainer;

        private string pathToImage = @"bitmap\bitmap.jpg";

        [Test]
        public void ConstructorTest()
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
        public void GetFpsByKeyTest()
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
    }
}
