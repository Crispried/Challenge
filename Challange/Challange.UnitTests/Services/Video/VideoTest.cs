using System.Collections.Generic;
using NUnit.Framework;
using System.Drawing;
using NSubstitute;
using Challange.Domain.Services.Video.Abstract;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class VideoTest : TestCase
    {
        private List<IFps> fpsList;
        private Domain.Services.Video.Concrete.Video videoWithFpsList;
        private Domain.Services.Video.Concrete.Video videsWithBitmapList;
        private Bitmap _bitmap;

        [SetUp]
        public void SetUp()
        {
            _bitmap = new Bitmap(3, 3);
            fpsList = Substitute.For<List<IFps>>();
            var fps = Substitute.For<IFps>();
            fps.AddFrame(new Bitmap(3, 3));
            fps.AddFrame(new Bitmap(3, 3));
            fpsList.Add(fps);
            var frames = new List<Bitmap>()
            {
                _bitmap,
                _bitmap
            };
            fps.Frames.Returns(frames);
            videoWithFpsList = Substitute.For<Domain.Services.Video.Concrete.Video>("1", fpsList);
            videsWithBitmapList = Substitute.For<Domain.Services.Video.Concrete.Video>("1", new List<Bitmap>() { _bitmap });
        }

        [Test]
        public void NamePropertyTest()
        {
            // Arrange
            // Act
            var name = videoWithFpsList.Name;
            // Assert
            Assert.IsTrue(name == "1");
        }

        [Test]
        public void FramesPropertyTest()
        {
            // Arrange
            // Act
            var frames = videsWithBitmapList.Frames;
            // Assert
            Assert.IsTrue(frames.Contains(_bitmap));
        }

        [Test]
        public void FrameIndexPropertyTest()
        {
            // Arrange
            videoWithFpsList.FrameIndex = 2;
            // Act
            var frameIndex = videoWithFpsList.FrameIndex;
            // Assert
            Assert.IsTrue(frameIndex == 2);
        }

        [Test]
        public void GetCurrentFrameTest()
        {
            // Arrange
            videoWithFpsList.FrameIndex = 1;
            // Act
            var currentFrame = videoWithFpsList.GetCurrentFrame();
            // Assert
            Assert.IsTrue(currentFrame == _bitmap);
        }

        [Test]
        public void IsEndReturnsTrueTest()
        {
            // Arrange
            videoWithFpsList.FrameIndex = 2;
            // Act
            var isEnd = videoWithFpsList.IsEnd();
            // Assert
            Assert.IsTrue(isEnd);
        }

        [Test]
        public void IsEndReturnsFalseTest()
        {
            // Arrange
            videoWithFpsList.FrameIndex = 3;
            // Act
            var isEnd = videoWithFpsList.IsEnd();
            // Assert
            Assert.IsFalse(isEnd);
        }

        [Test]
        public void CloneTest()
        {
            // Arrange
            var videoName = "TestVideo";
            var videoFrames = new List<Bitmap>();
            var video = new Domain.Services.Video.Concrete.Video(videoName, videoFrames);
            // Act
            var clone = video.Clone();
            // Assert
            Assert.IsTrue(clone.Name == video.Name);
            Assert.IsTrue(clone.Frames == video.Frames);
        }
    }
}
