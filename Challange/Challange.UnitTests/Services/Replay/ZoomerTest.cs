using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.Replay;
using Challange.Domain.Entities;

namespace Challange.UnitTests.Services.Replay
{
    [TestFixture]
    class ZoomerTest
    {
        private Zoomer zoomer;

        [SetUp]
        public void SetUp()
        {
            zoomer = new Zoomer();
        }

        [Test]
        public void ZoomCanNotBeSmallerThanMinimu()
        {
            // Arrange
            zoomer.Zoom = 0.5f;
            zoomer.MinZoom = 1f;

            // Act
            ZoomData zoomData = zoomer.MakeZoom();

            // Assert
            Assert.Equals(zoomData.GetZoom, zoomer.MinZoom);
        }
    }
}
