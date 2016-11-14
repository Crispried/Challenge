using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using Challange.Domain.Abstract;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class NullFpsTest
    {
        private IFps fps;

        [SetUp]
        public void SetUp()
        {
            fps = new NullFps();
        }

        [Test]
        public void FramesGetterTest()
        {
            // Arrange

            // Act

            // Assert
            Assert.Null(fps.Frames);
        }

        [Test]
        public void AddFrameMethodExistsTest()
        {
            // Arrange
            Bitmap frame = new Bitmap(Image.FromFile(@"bitmap\bitmap.jpg"));

            // Act
            fps.AddFrame(frame);

            // Assert
        }
    }
}
