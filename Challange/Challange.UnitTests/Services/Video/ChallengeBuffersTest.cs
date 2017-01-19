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
        private IFpsContainer fpsContainer;
        private ICamerasContainer camerasContainer;
        private IChallengeBuffers buffers;

        [SetUp]
        public void SetUp()
        {
            
            camerasContainer = Substitute.For<ICamerasContainer>();
            camerasContainer.GetCamerasKeys().Returns(new List<string> { "One", "Two" });
            buffers = new ChallengeBuffers(camerasContainer);

            fpsContainer = Substitute.For<IFpsContainer>();
            var fpses = new Dictionary<string, IFps>();
            fpses.Add("One", Substitute.For<IFps>());
            fpses.Add("Two", Substitute.For<IFps>());
            fpsContainer.Fpses.Returns(fpses);
        }

        [Test]
        public void RemoveFirstFpsFromPastBufferTest()
        {
            // Arrange
            buffers.AddPastFpses(fpsContainer);
            // Act
            buffers.RemoveFirstFpsFromPastBuffer();
            // Assert
            foreach (var pastCameraRecordValue in buffers.PastCameraRecords.Values)
            {
                Assert.IsTrue(pastCameraRecordValue.Count == 0);
            }
        }

        [Test]
        public void HaveToAddFutureFpsReturnsTrueTest()
        {
            // Arrange
            // Act
            bool result = buffers.HaveToAddFutureFps(2);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void HaveToAddFutureFpsReturnsFalseTest()
        {
            // Arrange
            buffers.AddFutureFpses(fpsContainer);
            // Act
            bool result = buffers.HaveToAddFutureFps(1);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void HaveToAddFutureFpsReturnsFalseOnNullTest()
        {
            // Arrange
            buffers.ClearBuffers();
            // Act
            bool result = buffers.HaveToAddFutureFps(1);

            // Assert
            Assert.False(result);
        }

        [Test]
        public void HaveToRemovePastFpsReturnsTrueTest()
        {
            // Arrange
            buffers.AddPastFpses(fpsContainer);
            // Act
            bool result = buffers.HaveToRemovePastFps(1);

            // Assert
            Assert.True(result);
        }

        [Test]
        public void HaveToRemovePastFpsReturnsFalseTest()
        {
            // Arrange
            // Act
            bool result = buffers.HaveToRemovePastFps(2);
            // Assert
            Assert.False(result);
        }

        [Test]
        public void HaveToRemovePastFpsReturnsFalseOnNullTest()
        {
            // Arrange
            buffers.ClearBuffers();
            // Act
            bool result = buffers.HaveToRemovePastFps(2);
            // Assert
            Assert.False(result);
        }

        [Test]
        public void ClearBuffersTest()
        {
            // Arrange
            // Act
            buffers.ClearBuffers();
            // Assert
            Assert.IsEmpty(buffers.PastCameraRecords);
            Assert.IsEmpty(buffers.FutureCameraRecords);
        }

        [Test]
        public void AddPastFpsesIfValueWasFoundTest()
        {
            // Arrange
            // Act
            buffers.AddPastFpses(fpsContainer);
            // Assert
            Assert.IsTrue(buffers.PastCameraRecords.ContainsKey("One"));
            Assert.IsTrue(buffers.PastCameraRecords.ContainsKey("Two"));
        }

        [Test]
        public void AddPastFpsesIfValueWasNotFoundTest()
        {
            // Arrange
            var fpses = new Dictionary<string, IFps>();
            fpses.Add("Three", Substitute.For<IFps>());
            fpsContainer.Fpses.Returns(fpses);
            // Act
            buffers.AddPastFpses(fpsContainer);
            // Assert
            Assert.IsFalse(buffers.PastCameraRecords.ContainsKey("Three"));
        }

        [Test]
        public void AddFutureFpsesIfValueWasFoundTest()
        {
            // Arrange
            // Act
            buffers.AddFutureFpses(fpsContainer);
            // Assert
            Assert.IsTrue(buffers.FutureCameraRecords.ContainsKey("One"));
            Assert.IsTrue(buffers.FutureCameraRecords.ContainsKey("Two"));
        }

        [Test]
        public void AddFutureFpsesIfValueWasNotFoundTest()
        {
            // Arrange
            var fpses = new Dictionary<string, IFps>();
            fpses.Add("Three", Substitute.For<IFps>());
            fpsContainer.Fpses.Returns(fpses);
            // Act
            buffers.AddPastFpses(fpsContainer);
            // Assert
            Assert.IsFalse(buffers.FutureCameraRecords.ContainsKey("Three"));
        }

        [Test]
        public void UniteBuffersNotEmptyBuffersTest()
        {
            // Arrange
            var camera1 = Substitute.For<ICamera>();
            camera1.Name = "lol";
            var camera2 = Substitute.For<ICamera>();
            camera2.Name = "gol";
            camerasContainer.GetCameraByKey(default(string)).ReturnsForAnyArgs(camera1, camera2);
            // Act
            var unitedBuffers = buffers.UniteBuffers();
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
            buffers.ClearBuffers();
            // Act
            var unitedBuffers = buffers.UniteBuffers();
            // Assert
            Assert.IsTrue(unitedBuffers.Count == 0);
        }
    }
}
