using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class ZoomDataTest : TestCase
    {
        private ZoomData zoomData;
        private float zoom;
        private int imgx;
        private int imgy;

        [SetUp]
        public void SetUp()
        {
            zoom = 1.0f;
            imgx = 1;
            imgy = 1;
            zoomData = new ZoomData(zoom, imgx, imgy);
        }

        [Test]
        public void ZoomGetterTest()
        {
            // Arrange

            // Act
            float receivedZoom = zoomData.Zoom;

            // Assert
            Assert.AreEqual(zoom, receivedZoom);
        }

        [Test]
        public void ImgxGetterTest()
        {
            // Arrange

            // Act
            float receivedImgx = zoomData.GetImgX;

            // Assert
            Assert.AreEqual(imgx, receivedImgx);
        }

        [Test]
        public void ImgyGetterTest()
        {
            // Arrange

            // Act
            float receivedImgy = zoomData.GetImgY;

            // Assert
            Assert.AreEqual(imgy, receivedImgy);
        }
    }
}
