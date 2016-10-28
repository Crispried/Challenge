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
        private float defZoom;

        [SetUp]
        public void SetUp()
        {
            zoomCalculator = Substitute.For<ZoomCalculator>();
            zoomer = new Zoomer(zoomCalculator);
            defZoom = zoomer.Zoom;
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
            ZoomData zoomData = zoomer.MakeZoom(pictureBoxLocation, delta, mouseLocation);

            // Assert
            Assert.AreEqual(zoomData.Zoom, zoomer.MinZoom);
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

    }
}
