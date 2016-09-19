using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using System.Collections.Generic;

namespace Challange.UnitTests.Entities
{
    class ChallengeBuffersTest
    {
        private FPS fps;
        private List<FPS> fpsList;
        private Bitmap bitmap;
        private CamerasContainer<Camera> camera;
        private ChallengeBuffers buffer;
        private List<FPS> outputFpsList;

        [SetUp]
        public void SetUp()
        {
            fps = new FPS();
            fpsList = new List<FPS>();
            bitmap = new Bitmap(@"bitmap\bitmap.jpg");
            camera = new CamerasContainer<Camera>();
            buffer = new ChallengeBuffers(camera);
            outputFpsList = new List<FPS>();
        }

        [Test]
        public void AddNewPastCameraRecordAddsFpsToPastBuffer()
        {
            // Arrange
            AddFpsToList();
            var pastCameraRecords = buffer.PastCameraRecords;

            // Act
            AddNewPastCameraRecord("key");
            List<FPS> outputFpsList = GetValueFromDictionary("key", pastCameraRecords);

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
            List<FPS> outputFpsList = GetValueFromDictionary("key", futureCameraRecords);

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
            List<FPS> outputFpsList = buffer.GetPastCameraRecordsValueByKey("key");

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
            List<FPS> outputFpsList = buffer.GetPastCameraRecordsValueByKey("key");

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
            List<FPS> outputFpsList = buffer.GetFutureCameraRecordsValueByKey("key");

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
            List<FPS> outputFpsList = buffer.GetFutureCameraRecordsValueByKey("key");

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
            List<FPS> outputFpsList = buffer.GetFirstPastValue();

            // Assert
            Assert.NotNull(outputFpsList);
        }

        [Test]
        public void GetFirstPastValueReturnsNullAsDefaultValue()
        {
            // Arrange
            AddFpsToList();
            var pastCameraRecords = buffer.PastCameraRecords;

            // Act
            List<FPS> outputFpsList = buffer.GetFirstPastValue();

            // Assert
            Assert.Null(outputFpsList);
        }

        [Test]
        public void GetFirstFutureValueReturnsFirstValue()
        {
            // Arrange
            AddFpsToList();
            var futureCameraRecords = buffer.FutureCameraRecords;

            // Act
            AddNewFutureCameraRecord("key");
            List<FPS> outputFpsList = buffer.GetFirstFutureValue();

            // Assert
            Assert.NotNull(outputFpsList);
        }

        [Test]
        public void GetFirstFutureValueReturnsNullAsDefaultValue()
        {
            // Arrange
            AddFpsToList();
            var futureCameraRecords = buffer.FutureCameraRecords;

            // Act
            List<FPS> outputFpsList = buffer.GetFirstFutureValue();

            // Assert
            Assert.Null(outputFpsList);
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

        private List<FPS> GetValueFromDictionary(string key, Dictionary<string, List<FPS>> dictionary)
        {
            dictionary.TryGetValue(key, out outputFpsList);

            return outputFpsList;
        }
    }
}
