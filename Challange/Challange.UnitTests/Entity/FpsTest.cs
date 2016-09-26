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
    class FpsTest
    {
        private Fps fps;
        private Bitmap frame;

        [SetUp]
        public void SetUp()
        {
            fps = new Fps();
            frame = new Bitmap(@"bitmap\bitmap.jpg");
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

        private void AddFrame()
        {
            fps.AddFrame(frame);
        }
    }
}
