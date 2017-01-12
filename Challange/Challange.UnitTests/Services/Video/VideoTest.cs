using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using Challange.Domain.Abstract;
using NSubstitute;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class VideoTest : TestCase
    {
        private List<IFps> fpsList;
        private string bitmapPath;
        private Domain.Services.Video.Concrete.Video video;

        [SetUp]
        public void SetUp()
        {
            bitmapPath = @"bitmap/bitmap.jpg";
            fpsList = InitializeFpsList();
            var name = "1";
            video = Substitute.For<Domain.Services.Video.Concrete.Video>(name, fpsList);
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
