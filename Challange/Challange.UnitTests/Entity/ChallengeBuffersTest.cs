using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.UnitTests.Entity
{
    class ChallengeBuffersTest
    {
        private Fps fps;
        private List<Fps> fpsList;
        private Bitmap bitmap;
        private CamerasContainer camera;
        private ChallengeBuffers buffer;
        private List<Fps> outputFpsList;
        private List<Device> camerasInfo;
        private int maxElementsInPastCollection;
        private int maxElementsInFutureCollection;

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            fps = new Fps();
            fpsList = new List<Fps>();
            bitmap = new Bitmap(@"bitmap\bitmap.jpg");
            camera = new CamerasContainer(camerasInfo);
            maxElementsInPastCollection = 10;
            maxElementsInFutureCollection = 10;
            buffer = new ChallengeBuffers(camera,
                        maxElementsInPastCollection,
                        maxElementsInFutureCollection);
            outputFpsList = new List<Fps>();
        }

        private List<Device> InitializeCamerasInfo()
        {
            List<Device> camerasInfo = new List<Device>();
            Device item = new Device();
            item.FullName = "FullName:port";
            item.Name = "Name";
            camerasInfo.Add(item);

            return camerasInfo;
        }

        [Test]
        public void AddNewPastCameraRecordAddsFpsToPastBuffer()
        {
            // Arrange
            AddFpsToList();
            var pastCameraRecords = buffer.PastCameraRecords;

            // Act
            AddNewPastCameraRecord("key");
            List<Fps> outputFpsList = GetValueFromDictionary("key", pastCameraRecords);

            // Assert
            Assert.NotNull(outputFpsList);
        }

        [Test]
        public void AddNewFutureCameraRecordAddsFpsToFutureBuffer()
        {
            // Arrange
            AddFpsToList();
            var futureCameraRecords = buffer.FutureCameraRecords;

            // Act
            AddNewFutureCameraRecord("key");
            List<Fps> outputFpsList = GetValueFromDictionary("key", futureCameraRecords);

            // Assert
            Assert.NotNull(outputFpsList);
        }

        [Test]
        public void GetPastCameraRecordsValueByKeyReturnProperValue()
        {
            // Arrange
            AddFpsToList();
            var pastCameraRecords = buffer.PastCameraRecords;

            // Act
            AddNewPastCameraRecord("key");
            List<Fps> outputFpsList = buffer.GetPastCameraRecordsValueByKey("key");

            // Assert
            Assert.NotNull(outputFpsList);
        }

        [Test]
        public void GetPastCameraRecordsValueByKeyReturnNull()
        {
            // Arrange
            AddFpsToList();
            var pastCameraRecords = buffer.PastCameraRecords;

            // Act
            List<Fps> outputFpsList = buffer.GetPastCameraRecordsValueByKey("key");

            // Assert
            Assert.Null(outputFpsList);
        }

        [Test]
        public void GetFutureCameraRecordsValueByKeyReturnProperValue()
        {
            // Arrange
            AddFpsToList();
            var futureCameraRecords = buffer.FutureCameraRecords;

            // Act
            AddNewFutureCameraRecord("key");
            List<Fps> outputFpsList = buffer.GetFutureCameraRecordsValueByKey("key");

            // Assert
            Assert.NotNull(outputFpsList);
        }

        [Test]
        public void GetFutureCameraRecordsValueByKeyReturnNull()
        {
            // Arrange
            AddFpsToList();
            var futureCameraRecords = buffer.FutureCameraRecords;

            // Act
            List<Fps> outputFpsList = buffer.GetFutureCameraRecordsValueByKey("key");

            // Assert
            Assert.Null(outputFpsList);
        }

        [Test]
        public void GetFirstPastValueReturnsFirstValue()
        {
            // Arrange
            AddFpsToList();
            var pastCameraRecords = buffer.PastCameraRecords;

            // Act
            AddNewPastCameraRecord("key");
            List<Fps> outputFpsList = buffer.GetFirstPastValue();

            // Assert
            Assert.NotNull(outputFpsList);
        }

        [Test]
        public void GetFirstPastValueReturnsEmptyListAsDefaultValue()
        {
            // Arrange
            AddFpsToList();
            var pastCameraRecords = buffer.PastCameraRecords;

            // Act
            List<Fps> outputFpsList = buffer.GetFirstPastValue();

            // Assert
            Assert.IsEmpty(outputFpsList);
        }

        [Test]
        public void GetFirstFutureValueReturnsFirstValue()
        {
            // Arrange
            AddFpsToList();
            var futureCameraRecords = buffer.FutureCameraRecords;

            // Act
            AddNewFutureCameraRecord("key");
            List<Fps> outputFpsList = buffer.GetFirstFutureValue();

            // Assert
            Assert.NotNull(outputFpsList);
        }

        [Test]
        public void GetFirstFutureValueReturnsEmptyListAsDefaultValue()
        {
            // Arrange
            AddFpsToList();
            var futureCameraRecords = buffer.FutureCameraRecords;

            // Act
            List<Fps> outputFpsList = buffer.GetFirstFutureValue();

            // Assert
            Assert.IsEmpty(outputFpsList);
        }

        private void AddFrame()
        {
            fps.AddFrame(bitmap);
        }

        private void AddFpsToList()
        {
            AddFrame();
            fpsList.Add(fps);
        }

        private void AddNewPastCameraRecord(string key)
        {
            buffer.AddNewPastCameraRecord(key, fpsList);
        }

        private void AddNewFutureCameraRecord(string key)
        {
            buffer.AddNewFutureCameraRecord(key, fpsList);
        }

        private List<Fps> GetValueFromDictionary(string key, Dictionary<string, List<Fps>> dictionary)
        {
            dictionary.TryGetValue(key, out outputFpsList);

            return outputFpsList;
        }
    }
}
