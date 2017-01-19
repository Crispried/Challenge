using NUnit.Framework;
using System.Drawing;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class FpsTest : TestCase
    { 
        private IFps fps;
        private string imagePath;
        private Bitmap frame;

        [SetUp]
        public void SetUp()
        {
            fps = new Fps();
            frame = new Bitmap(3, 3);
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
            Assert.IsTrue(fps.Frames[0] == frame);
        }

        private void AddFrame()
        {
            fps.AddFrame(frame);
        }
    }
}
