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

namespace Challange.UnitTests.Entity
{
    class ChallengeBuffersTest : TestCase
    {
        private IFps fps;
        private FpsContainer fpsContainer;
        private Bitmap bitmap;
        private CamerasContainer camerasContainer;
        private ChallengeBuffers buffers;
        private List<Device> camerasInfo;
        private int maxElementsInPastCollection;
        private int maxElementsInFutureCollection;
        private string imagePath = @"bitmap\bitmap.jpg";
        private string key1 = "FullName1";
        private string key2 = "FullName2";
        private string incorrectKey1 = "key1";
        private string incorrectKey2 = "key2";

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            bitmap = new Bitmap(imagePath);
            camerasContainer = new CamerasContainer(camerasInfo);
            fpsContainer = new FpsContainer(camerasContainer.GetCamerasKeys);
            fps = fpsContainer.GetFpsByKey(key1);
            fps.AddFrame(bitmap);
            fps = fpsContainer.GetFpsByKey(key2);
            fps.AddFrame(bitmap);
            maxElementsInPastCollection = 10;
            maxElementsInFutureCollection = 10;
            buffers = new ChallengeBuffers(camerasContainer,
                        maxElementsInPastCollection,
                        maxElementsInFutureCollection);
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
            List <IFps> outputFpsList = buffers.GetFirstFutureValue();

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
        public void PastCameraRecordsGetterTest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void AddPastFpsesTest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Test]
        public void HaveToAddFutureFpsReturnsFalseTest()
        {
            // Arrange
            buffers.MaxElementsInFutureCollection = 1;

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
            buffers.MaxElementsInPastCollection = 2;

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
        public void RemoveFirstFpsFromPastBufferTest()
        {
            // Arrange

            // Act
            buffers.RemoveFirstFpsFromPastBuffer();

            // Assert
            // Make an assertion
        }
    }
}
