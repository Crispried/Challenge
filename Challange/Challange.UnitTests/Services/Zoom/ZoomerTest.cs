using NUnit.Framework;
using System.Drawing;
using NSubstitute;
using Challange.Domain.Services.Zoom.Abstract;
using Challange.Domain.Services.Zoom.Concrete;

namespace Challange.UnitTests.Services.Zoom
{
    [TestFixture]
    class ZoomerTest : TestCase
    {
        private IZoomer zoomer;
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
            float receivedImgx = zoomer.ImgX;

            // Assert
            Assert.AreEqual(0, receivedImgx);
        }

        [Test]
        public void ImgyGetterTest()
        {
            // Arrange

            // Act
            float receivedImgy = zoomer.ImgY;

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
            ZoomData zoomData = MakeZoom(pictureBoxLocation, delta, mouseLocation);

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
            ZoomData zoomData = MakeZoom(pictureBoxLocation, delta, mouseLocation);

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
            ZoomData zoomData = MakeZoom(pictureBoxLocation, delta, mouseLocation);

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
            ZoomData zoomData = MakeZoom(pictureBoxLocation, delta, mouseLocation);

            // Assert
            zoomCalculator.ReceivedWithAnyArgs().CalculatePositiveZoom(zoomer.Zoom);
            zoomCalculator.DidNotReceiveWithAnyArgs().CalculateNegativeZoom(zoomer.Zoom, zoomer.MinZoom);
        }

        [Test]
        public void CalculateNewImageLocationIsBeingFired()
        {
            // Arrange

            // Act
            ZoomData zoomData = MakeZoom();

            // Assert
            zoomCalculator.ReceivedWithAnyArgs().CalculateNewImageLocation(1.1f, 0, 0,
                                                        1, new Point(), new Point());
        }

        [Test]
        public void ProperImgXAndImgYAreSet()
        {
            // Arrange
            
            // Act
            zoomCalculator.CalculateNewImageLocation(1.1f, 0, 0,
                                        1, new Point(), new Point()).ReturnsForAnyArgs(new Point(1, 1));
            ZoomData zoomData = MakeZoom();

            // Assert
            Assert.AreEqual(1f, zoomer.ImgX);
            Assert.AreEqual(1f, zoomer.ImgY);
        }

        private ZoomData MakeZoom(Point pictureBoxLocation = new Point(), int delta = 1, Point mouseLocation = new Point())
        {
            return zoomer.MakeZoom(pictureBoxLocation, delta, mouseLocation);
        }
    }
}
