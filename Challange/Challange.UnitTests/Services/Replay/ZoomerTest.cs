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
    class ZoomerTest
    {
        private Zoomer zoomer;
        private IZoomCalculator zoomCalculator;

        [SetUp]
        public void SetUp()
        {
            zoomCalculator = Substitute.For<IZoomCalculator>();
            zoomer = new Zoomer(zoomCalculator);
        }

        [Test]
        public void MinZoomPropertyTest()
        {
            // Arrange
            float minZoom = 0.8f;
            zoomer.MinZoom = minZoom;

            // Act
            float receivedMinZoom = zoomer.MinZoom;

            // Assert
            Assert.AreEqual(minZoom, receivedMinZoom);
        }

        [Test]
        public void ZoomPropertyTest()
        {
            // Arrange
            float zoom = 0.8f;
            zoomer.Zoom = zoom;

            // Act
            float receivedZoom = zoomer.Zoom;

            // Assert
            Assert.AreEqual(zoom, receivedZoom);
        }

        [Test]
        public void ImgxGetterTest()
        {
            // Arrange

            // Act
            float receivedImgx = zoomer.Imgx;

            // Assert
            Assert.AreEqual(0, receivedImgx);
        }

        [Test]
        public void ImgyGetterTest()
        {
            // Arrange

            // Act
            float receivedImgy = zoomer.Imgy;

            // Assert
            Assert.AreEqual(0, receivedImgy);
        }

        [Test]
        public void ZoomCanNotBeSmallerThanMinimum()
        {
            // Arrange
            Point pictureBoxLocation = new Point();
            int delta = -1;
            Point mouseLocation = new Point();
            zoomer.Zoom = 0.5f;
            zoomer.MinZoom = 1f;

            // Act
            zoomCalculator.CalculateNegativeZoom(zoomer.Zoom, zoomer.MinZoom).ReturnsForAnyArgs(zoomer.MinZoom);
            ZoomData zoomData = zoomer.MakeZoom(pictureBoxLocation, delta, mouseLocation);

            // Assert
            Assert.AreEqual(zoomData.Zoom, zoomer.MinZoom);
        }

        [Test]
        public void ZoomCanBeBiggerThanMinimum()
        {
            // Arrange
            Point pictureBoxLocation = new Point();
            int delta = -1;
            Point mouseLocation = new Point();
            zoomer.Zoom = 1.5f;
            zoomer.MinZoom = 1f;

            // Act
            zoomCalculator.CalculateNegativeZoom(zoomer.Zoom, zoomer.MinZoom).ReturnsForAnyArgs(zoomer.Zoom);
            ZoomData zoomData = zoomer.MakeZoom(pictureBoxLocation, delta, mouseLocation);

            // Assert
            Assert.Greater(zoomData.Zoom, zoomer.MinZoom);
        }

        [Test]
        public void NegativeZoomOnlyIfMouseIsScrollingDown()
        {
            // Arrange
            Point pictureBoxLocation = new Point();
            int delta = -1;
            Point mouseLocation = new Point();

            // Act
            ZoomData zoomData = zoomer.MakeZoom(pictureBoxLocation, delta, mouseLocation);

            // Assert
            zoomCalculator.ReceivedWithAnyArgs().CalculateNegativeZoom(zoomer.Zoom, zoomer.MinZoom);
            zoomCalculator.DidNotReceiveWithAnyArgs().CalculatePositiveZoom(zoomer.Zoom);
        }

        [Test]
        public void PositiveZoomOnlyIfMouseIsScrollingDown()
        {
            // Arrange
            Point pictureBoxLocation = new Point();
            int delta = 1;
            Point mouseLocation = new Point();

            // Act
            ZoomData zoomData = zoomer.MakeZoom(pictureBoxLocation, delta, mouseLocation);

            // Assert
            zoomCalculator.ReceivedWithAnyArgs().CalculatePositiveZoom(zoomer.Zoom);
            zoomCalculator.DidNotReceiveWithAnyArgs().CalculateNegativeZoom(zoomer.Zoom, zoomer.MinZoom);
        }

        [Test]
        public void CalculateNewImageLocationIsBeingFired()
        {
            // Arrange
            int delta = 1;

            // Act
            ZoomData zoomData = zoomer.MakeZoom(new Point(), delta, new Point());

            // Assert
            zoomCalculator.ReceivedWithAnyArgs().CalculateNewImageLocation(1.1f, 0, 0,
                                                        1, new Point(), new Point());
        }

        [Test]
        public void ProperImgXAndImgYAreSet()
        {
            // Arrange
            int delta = 1;
            
            // Act
            zoomCalculator.CalculateNewImageLocation(1.1f, 0, 0,
                                        1, new Point(), new Point()).ReturnsForAnyArgs(new Point(1, 1));
            ZoomData zoomData = zoomer.MakeZoom(new Point(), delta, new Point());

            // Assert
            Assert.AreEqual(1f, zoomer.Imgx);
            Assert.AreEqual(1f, zoomer.Imgy);
        }

    }
}
