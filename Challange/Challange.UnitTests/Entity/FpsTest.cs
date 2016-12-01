using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using NSubstitute;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class FpsTest : TestCase
    { 
        private Fps fps;
        private string imagePath;
        private Bitmap frame;

        [SetUp]
        public void SetUp()
        {
            fps = new Fps();
            imagePath = @"bitmap\bitmap.jpg";
            frame = new Bitmap(imagePath);
        }

        [Test]
        public void AddFrameAddsFrame()
        {
            // Arrange

            // Act
            AddFrame();

            // Assert
            Assert.IsNotEmpty(fps.Frames);
        }

        [Test]
        public void DisposeFramesTest()
        {
            // Arrange
            AddFrame();

            // Act
            fps.DisposeFrames();

            // Assert
            Assert.IsTrue(fps.Frames[0].PixelFormat == System.Drawing.Imaging.PixelFormat.DontCare);
        }

        private void AddFrame()
        {
            fps.AddFrame(frame);
        }
    }
}
