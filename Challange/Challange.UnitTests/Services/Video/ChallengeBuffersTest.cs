using System.Collections.Generic;
using NUnit.Framework;
using Challange.Domain.Services.StreamProcess.Abstract;
using NSubstitute;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;

namespace Challange.UnitTests.Services.Video
{
    class ChallengeBuffersTest : TestCase
    {
        private IFpsContainer _fpsContainer;
        private IChallengeBuffers _buffers;

        [SetUp]
        public void SetUp()
        {
            _buffers = new ChallengeBuffers();

            _fpsContainer = Substitute.For<IFpsContainer>();
            var fpses = new Dictionary<string, IFps>();
            fpses.Add("One", Substitute.For<IFps>());
            fpses.Add("Two", Substitute.For<IFps>());
            _fpsContainer.Fpses.Returns(fpses);

            _buffers.AddFutureFpses(_fpsContainer);
            _buffers.AddPastFpses(_fpsContainer);
        }

        [Test]
        public void AddPastFpsesIfKeyExistsTest()
        {
            // Arrange
            var fpsContainer = Substitute.For<IFpsContainer>();
            var fpses = new Dictionary<string, IFps>();
            var fps = Substitute.For<IFps>();
            fpses.Add("One", fps);
            fpsContainer.Fpses.Returns(fpses);
            // Act
            _buffers.AddPastFpses(fpsContainer);
            // Assert
            Assert.True(_buffers.PastCameraRecords["One"].Contains(fps));
        }

        [Test]
        public void AddPastFpsesIfKeyNotExistsTest()
        {
            // Arrange
            var fpsContainer = Substitute.For<IFpsContainer>();
            var fpses = new Dictionary<string, IFps>();
            var fps = Substitute.For<IFps>();
            fpses.Add("Three", fps);
            fpsContainer.Fpses.Returns(fpses);
            // Act
            _buffers.AddPastFpses(fpsContainer);
            // Assert
            Assert.True(_buffers.PastCameraRecords.ContainsKey("Three"));
            Assert.True(_buffers.PastCameraRecords["Three"].Contains(fps));
        }

        [Test]
        public void AddFutureFpsesIfKeyExistsTest()
        {
            // Arrange
            var fpsContainer = Substitute.For<IFpsContainer>();
            var fpses = new Dictionary<string, IFps>();
            var fps = Substitute.For<IFps>();
            fpses.Add("One", fps);
            fpsContainer.Fpses.Returns(fpses);
            // Act
            _buffers.AddFutureFpses(fpsContainer);
            // Assert
            Assert.True(_buffers.FutureCameraRecords["One"].Contains(fps));
        }

        [Test]
        public void AddFutureFpsesIfKeyNotExistsTest()
        {
            // Arrange
            var fpsContainer = Substitute.For<IFpsContainer>();
            var fpses = new Dictionary<string, IFps>();
            var fps = Substitute.For<IFps>();
            fpses.Add("Three", fps);
            fpsContainer.Fpses.Returns(fpses);
            // Act
            _buffers.AddFutureFpses(fpsContainer);
            // Assert
            Assert.True(_buffers.FutureCameraRecords.ContainsKey("Three"));
            Assert.True(_buffers.FutureCameraRecords["Three"].Contains(fps));
        }

        [Test]
        public void RemoveFirstFpsFromPastBufferTest()
        {
            // Arrange
            // Act
            _buffers.RemoveFirstFpsFromPastBuffer();
            // Assert
            foreach (var pastCameraRecordValue in _buffers.PastCameraRecords.Values)
            {
                Assert.IsTrue(pastCameraRecordValue.Count == 0);
            }
        }

        [Test]
        public void HaveToAddFutureFpsReturnsTrueTest()
        {
            // Arrange
            // Act
            bool result = _buffers.HaveToAddFutureFps(2);
            // Assert
            Assert.True(result);
        }

        [Test]
        public void HaveToAddFutureFpsReturnsFalseTest()
        {
            // Arrange
            // Act
            bool result = _buffers.HaveToAddFutureFps(1);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void HaveToAddFutureFpsReturnsFalseOnNullTest()
        {
            // Arrange
            _buffers.ClearBuffers();
            // Act
            bool result = _buffers.HaveToAddFutureFps(1);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void HaveToRemovePastFpsReturnsTrueTest()
        {
            // Arrange
            // Act
            bool result = _buffers.HaveToRemovePastFps(1);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void HaveToRemovePastFpsReturnsFalseTest()
        {
            // Arrange
            // Act
            bool result = _buffers.HaveToRemovePastFps(2);
            // Assert
            Assert.False(result);
        }

        [Test]
        public void HaveToRemovePastFpsReturnsFalseOnNullTest()
        {
            // Arrange
            _buffers.ClearBuffers();
            // Act
            bool result = _buffers.HaveToRemovePastFps(2);
            // Assert
            Assert.False(result);
        }

        [Test]
        public void ClearBuffersTest()
        {
            // Arrange
            // Act
            _buffers.ClearBuffers();
            // Assert
            Assert.IsEmpty(_buffers.PastCameraRecords);
            Assert.IsEmpty(_buffers.FutureCameraRecords);
        }       

        [Test]
        public void UniteBuffersNotEmptyBuffersTest()
        {
            // Arrange
            var camerasContainer = Substitute.For<ICamerasContainer>();
            camerasContainer.GetCamerasKeys().Returns(new List<string> { "One", "Two" });
            var camera1 = Substitute.For<ICamera>();
            camera1.Name = "lol";
            var camera2 = Substitute.For<ICamera>();
            camera2.Name = "gol";
            camerasContainer.GetCameraByKey(default(string)).ReturnsForAnyArgs(camera1, camera2);
            // Act
            var unitedBuffers = _buffers.UniteBuffers(camerasContainer);
            // Assert
            camerasContainer.ReceivedWithAnyArgs().GetCameraByKey(default(string));
            var cameraName1 = camera1.Received().Name;
            var cameraName2 = camera2.Received().Name;
            Assert.IsTrue(unitedBuffers.Count == 2);
            Assert.IsTrue(unitedBuffers.ContainsKey("lol"));
            Assert.IsTrue(unitedBuffers.ContainsKey("gol"));
        }

        [Test]
        public void UniteBuffersEmptyBuffersTest()
        {
            // Arrange
            var camerasContainer = Substitute.For<ICamerasContainer>();
            camerasContainer.GetCamerasKeys().Returns(new List<string> { "One", "Two" });
            _buffers.ClearBuffers();
            // Act
            var unitedBuffers = _buffers.UniteBuffers(camerasContainer);
            // Assert
            Assert.IsTrue(unitedBuffers.Count == 0);
        }
    }
}
