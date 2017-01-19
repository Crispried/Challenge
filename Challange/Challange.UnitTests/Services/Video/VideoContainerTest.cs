using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class VideoContainerTest : TestCase
    {
        private IVideoContainer _videoContainer;

        [SetUp]
        public void SetUp()
        {
            _videoContainer = new VideoContainer();
        }

        [Test]
        public void ConvertToVideoContainerTest()
        {
            // Arrange
            var buffers = Substitute.For<IChallengeBuffers>();
            var fpses = new Dictionary<string, List<IFps>>();
            fpses.Add("One", Substitute.For<List<IFps>>());
            fpses.Add("Two", Substitute.For<List<IFps>>());
            buffers.UniteBuffers().Returns(fpses);
            // Act
            _videoContainer.ConvertToVideoContainer(buffers);
            // Assert
            buffers.Received().UniteBuffers();
        }

        [Test]
        public void AddVideoTest()
        {
            // Arrange
            var video = new Domain.Services.Video.Concrete.Video("hello", Substitute.For<List<IFps>>());
            // Act
            _videoContainer.AddVideo(video);
            // Assert
            Assert.IsTrue(_videoContainer.Videos.Contains(video));
        }

        [Test]
        public void IsEmptyReturnsTrue()
        {
            // Arrange
            // Act
            var isEmpty = _videoContainer.IsEmpty();
            // Assert
            Assert.IsTrue(isEmpty);
        }

        [Test]
        public void IsEmptyReturnsFalse()
        {
            // Arrange
            _videoContainer.Videos.Add(new Domain.Services.Video.Concrete.Video("test", Substitute.For<List<IFps>>()));
            // Act
            var isEmpty = _videoContainer.IsEmpty();
            // Assert
            Assert.IsFalse(isEmpty);
        }
    }
}
