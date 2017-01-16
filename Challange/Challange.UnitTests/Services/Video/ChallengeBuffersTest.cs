using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using NSubstitute;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;

namespace Challange.UnitTests.Services.Video
{
    class ChallengeBuffersTest : TestCase
    {
        private IFps fps;
        private IFpsContainer fpsContainer;
        private Bitmap bitmap;
        private ICamerasContainer camerasContainer;
        private IChallengeBuffers buffers;
        private List<Device> camerasInfo;
        private int maxElementsInPastCollection;
        private int maxElementsInFutureCollection;
        private string imagePath = @"bitmap\bitmap.jpg";
        private string key1 = "One";
        private string key2 = "Two";
        private string incorrectKey1 = "key1";
        private string incorrectKey2 = "key2";
        private Dictionary<string, IFps> dictionary;

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            bitmap = new Bitmap(imagePath);
            camerasContainer = Substitute.For<ICamerasContainer>();
            camerasContainer.GetCamerasKeys().Returns(new List<string> { "One", "Two" });
            fpsContainer = Substitute.For<IFpsContainer>();
            dictionary = new Dictionary<string, IFps>();
            dictionary.Add("One", fps);
            fpsContainer.Fpses.Returns(dictionary);
            fps = fpsContainer.GetFpsByKey(key1);
            fps.AddFrame(bitmap);
            fps = fpsContainer.GetFpsByKey(key2);
            fps.AddFrame(bitmap);
            maxElementsInPastCollection = 10;
            maxElementsInFutureCollection = 10;
            buffers = new ChallengeBuffers(camerasContainer);
            buffers.AddFutureFpses(fpsContainer);
            buffers.AddPastFpses(fpsContainer);
        }

        [Test]
        public void GetPastCameraRecordsValueByKeyReturnProperValue()
        {
            // Arrange

            // Act
            List<IFps> outputFpsList1 = buffers.GetPastCameraRecordsValueByKey(key1);
            List<IFps> outputFpsList2 = buffers.GetPastCameraRecordsValueByKey(key1);

            // Assert
            Assert.NotNull(outputFpsList1);
            Assert.NotNull(outputFpsList2);
        }

        [Test]
        public void GetPastCameraRecordsValueByKeyReturnNull()
        {
            // Arrange
            // Act
            List<IFps> outputFpsList1 =
                buffers.GetPastCameraRecordsValueByKey(incorrectKey1);
            List<IFps> outputFpsList2 =
                buffers.GetPastCameraRecordsValueByKey(incorrectKey2);
            // Assert
            Assert.Null(outputFpsList1);
            Assert.Null(outputFpsList2);
        }

        [Test]
        public void GetFutureCameraRecordsValueByKeyReturnProperValue()
        {
            // Arrange
            // Act
            List<IFps> outputFpsList1 = buffers.GetFutureCameraRecordsValueByKey(key1);
            List<IFps> outputFpsList2 = buffers.GetFutureCameraRecordsValueByKey(key2);
            // Assert
            Assert.NotNull(outputFpsList1);
            Assert.NotNull(outputFpsList2);
        }

        [Test]
        public void GetFutureCameraRecordsValueByKeyReturnNull()
        {
            // Arrange
            // Act
            List<IFps> outputFpsList1 =
                buffers.GetFutureCameraRecordsValueByKey(incorrectKey1);
            List<IFps> outputFpsList2 =
                buffers.GetFutureCameraRecordsValueByKey(incorrectKey2);
            // Assert
            Assert.Null(outputFpsList1);
            Assert.Null(outputFpsList2);
        }

        [Test]
        public void GetFirstPastValueReturnsFirstValue()
        {
            // Arrange
            // Act
            List<IFps> outputFpsList = buffers.GetFirstPastValue();
            // Assert
            Assert.True(outputFpsList ==
                buffers.PastCameraRecords.Values.ElementAt(0));
        }

        [Test]
        public void GetFirstFutureValueReturnsFirstValue()
        {
            // Arrange
            // Act
            List<IFps> outputFpsList = buffers.GetFirstFutureValue();

            // Assert
            Assert.True(outputFpsList ==
                buffers.FutureCameraRecords.Values.ElementAt(0));
        }

        [Test]
        public void MaxElementsInPastCollectionPropertyTest()
        {
            // Arrange
            int maxElements = 1;
            buffers.MaxElementsInPastCollection = maxElements;

            // Act
            int receivedMaxElements = buffers.MaxElementsInPastCollection;

            // Assert
            Assert.AreEqual(maxElements, receivedMaxElements);
        }

        [Test]
        public void MaxElementsInFutureCollectionPropertyTest()
        {
            // Arrange
            int maxElements = 1;
            buffers.MaxElementsInFutureCollection = maxElements;

            // Act
            int receivedMaxElements = buffers.MaxElementsInFutureCollection;

            // Assert
            Assert.AreEqual(maxElements, receivedMaxElements);
        }

        [Test]
        public void SetNumberOfPastAndFutureFramesTest()
        {
            // Arrange
            // Act
            buffers.SetNumberOfPastAndFutureElements(5, 5);
            // Assert
            Assert.IsTrue(buffers.MaxElementsInFutureCollection == 5);
            Assert.IsTrue(buffers.MaxElementsInPastCollection == 5);
        }

        [Test]
        public void RemoveFirstFpsFromPastBufferTest()
        {
            // Arrange
            buffers.PastCameraRecords.Clear();
            var firstFpsToDispose = Substitute.For<IFps>();
            var secondFpsToDispose = Substitute.For<IFps>();
            var thirdFps = Substitute.For<IFps>();
            var fourthFps = Substitute.For<IFps>();
            buffers.PastCameraRecords.Add("1", new List<IFps>() { firstFpsToDispose, thirdFps });
            buffers.PastCameraRecords.Add("2", new List<IFps>() { secondFpsToDispose, fourthFps });
            // Act
            buffers.RemoveFirstFpsFromPastBuffer();
            // Assert
            Assert.IsFalse(buffers.PastCameraRecords["1"].Contains(firstFpsToDispose));
            Assert.IsFalse(buffers.PastCameraRecords["2"].Contains(secondFpsToDispose));
            firstFpsToDispose.Received().DisposeFrames();
            secondFpsToDispose.Received().DisposeFrames();
        }

        [Test]
        public void HaveToAddFutureFpsReturnsFalseTest()
        {
            // Arrange
            buffers.FutureCameraRecords.Clear();

            // Act
            bool result = buffers.HaveToAddFutureFps();

            // Assert
            Assert.False(result);
        }

        [Test]
        public void HaveToAddFutureFpsReturnsTrueTest()
        {
            // Arrange
            buffers.MaxElementsInFutureCollection = 2;

            // Act
            bool result = buffers.HaveToAddFutureFps();

            // Assert
            Assert.True(result);
        }

        [Test]
        public void HaveToRemovePastFpsReturnsTrue()
        {
            // Arrange
            buffers.MaxElementsInPastCollection = 1;

            // Act
            bool result = buffers.HaveToRemovePastFps();

            // Assert
            Assert.True(result);
        }

        [Test]
        public void HaveToRemovePastFpsReturnsFalse()
        {
            // Arrange
            buffers.PastCameraRecords.Clear();

            // Act
            bool result = buffers.HaveToRemovePastFps();

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
        public void AddPastFpsesTest()
        {
            // Arrange
            var dictionary = new Dictionary<string, IFps>();
            dictionary.Add("Bla", fps);
            fpsContainer.Fpses.Returns(dictionary);
            // Act
            buffers.AddPastFpses(fpsContainer);
            // Assert
            Assert.IsTrue(buffers.PastCameraRecords.ContainsKey("Bla"));
        }

        [Test]
        public void AddFutureFpsesTest()
        {
            // Arrange
            var dictionary = new Dictionary<string, IFps>();
            dictionary.Add("Bla", fps);
            fpsContainer.Fpses.Returns(dictionary);
            // Act
            buffers.AddFutureFpses(fpsContainer);
            // Assert
            Assert.IsTrue(buffers.FutureCameraRecords.ContainsKey("Bla"));
        }
    }
}
