using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using Challange.Domain.Abstract;
using Challange.Domain.Servuces.Video.Concrete;
using NSubstitute;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class VideoTest : TestCase
    {
        private List<IFps> fpsList;
        private string bitmapPath;
        private Challange.Domain.Servuces.Video.Concrete.Video video;

        [SetUp]
        public void SetUp()
        {
            bitmapPath = @"bitmap/bitmap.jpg";
            fpsList = InitializeFpsList();
            var name = "1";
            video = Substitute.For<Challange.Domain.Servuces.Video.Concrete.Video>(name, fpsList);
        }

        [Test]
        public void NameProperty()
        {
            // Arrange
            // Act
            var getter = video.Name;
            // Assert
            Assert.IsTrue(getter == "1");
        }

        [Test]
        public void FpsListProperty()
        {
            // Arrange
            // Act
            var getter = video.Fpses;
            // Assert
            Assert.IsTrue(getter == fpsList);
        }

        [Test]
        public void FpsValueIsProperlyCounted()
        {
            // Arrange
            Challange.Domain.Servuces.Video.Concrete.Video video =
                new Challange.Domain.Servuces.Video.Concrete.Video("Video", fpsList);

            // Act

            // Assert
            Assert.AreEqual(1, video.FpsValue);
        }

        private List<IFps> InitializeFpsList()
        {
            IFps frame = new Fps();
            Bitmap bitmap = new Bitmap(bitmapPath);
            frame.AddFrame(bitmap);

            List<IFps> fpsList = new List<IFps>();
            fpsList.Add(frame);

            return fpsList;
        }

    }
}
