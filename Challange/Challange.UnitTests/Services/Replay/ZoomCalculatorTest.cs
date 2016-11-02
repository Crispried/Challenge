using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.Replay;
using Challange.Domain.Entities;
using System.Drawing;
using NSubstitute;

namespace Challange.UnitTests.Services.Replay
{
    [TestFixture]
    class ZoomCalculatorTest
    {
        private ZoomCalculator zoomCalculator;

        [SetUp]
        public void SetUp()
        {
            zoomCalculator = new ZoomCalculator();
        }

        [Test]
        public void CalculatePositiveZoomTest()
        {
            // Arrange
            float actualZoom = 1F;

            // Act
            float receivedZoom = zoomCalculator.CalculatePositiveZoom(actualZoom);

            // Assert
            Assert.AreEqual(1.1F, receivedZoom);
        }

        [Test]
        public void ZoomCannotBeSmallerThanMinimum()
        {
            // Arrange
            float actualZoom = 0.5f;
            float minZoom = 1f;

            // Act
            float receivedZoom = zoomCalculator.CalculateNegativeZoom(actualZoom, minZoom);

            // Assert
            Assert.AreEqual(minZoom, receivedZoom);
        }

        [Test]
        public void ZoomCanBeBiggerThanMinimum()
        {
            // Arrange
            float actualZoom = 1.5f;
            float minZoom = 1f;

            // Act
            float receivedZoom = zoomCalculator.CalculateNegativeZoom(actualZoom, minZoom);

            // Assert
            Assert.Greater(receivedZoom, minZoom);
        }

        [Test]
        public void CalculateNewImageLocationReturnsProperData()
        {
            // Arrange
            float zoom = 1.1f;
            float imgx = 0;
            float imgy = 0;
            float oldzoom = 1;
            Point mouseLocation = new Point(65, 65);
            Point pictureBoxLocation = new Point(5, 5);

            // Act
            Point data = zoomCalculator.CalculateNewImageLocation(zoom, imgx, imgy,
                                                        oldzoom, mouseLocation, pictureBoxLocation);

            // Assert
            Assert.AreEqual(-6, data.X);
            Assert.AreEqual(-6, data.Y);
        }

    }
}
