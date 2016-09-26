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
    class VideoTest
    {
        private List<Fps> fpsList;
        private string bitmapPath;

        [SetUp]
        public void SetUp()
        {
            bitmapPath = @"bitmap/bitmap.jpg";
            fpsList = InitializeFpsList();
        }

        [Test]
        public void FpsValueIsProperlyCounted()
        {
            // Arrange
            Video video = new Video("Video", fpsList);

            // Act

            // Assert
            Assert.AreEqual(1, video.FpsValue);
        }

        private List<Fps> InitializeFpsList()
        {
            Fps frame = new Fps();
            Bitmap bitmap = new Bitmap(bitmapPath);
            frame.AddFrame(bitmap);

            List<Fps> fpsList = new List<Fps>();
            fpsList.Add(frame);

            return fpsList;
        }

    }
}
