using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Entities;
using System.Drawing;
using System.Collections.Generic;

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

        [SetUp]
        public void SetUp()
        {
            fps = new Fps();
            fpsList = new List<Fps>();
            bitmap = new Bitmap(@"bitmap\bitmap.jpg");
            camera = new CamerasContainer();
            buffer = new ChallengeBuffers(camera);
            outputFpsList = new List<Fps>();
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
        public void GetFirstPastValueReturnsNullAsDefaultValue()
        {
            // Arrange
            AddFpsToList();
            var pastCameraRecords = buffer.PastCameraRecords;

            // Act
            List<Fps> outputFpsList = buffer.GetFirstPastValue();

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
            List<Fps> outputFpsList = buffer.GetFirstFutureValue();

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
            List<Fps> outputFpsList = buffer.GetFirstFutureValue();

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

        private List<Fps> GetValueFromDictionary(string key, Dictionary<string, List<Fps>> dictionary)
        {
            dictionary.TryGetValue(key, out outputFpsList);

            return outputFpsList;
        }
    }
}
