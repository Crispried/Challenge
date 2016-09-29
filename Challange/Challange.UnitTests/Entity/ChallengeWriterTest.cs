using Challange.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using System.Drawing;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class ChallengeWriterTest : TestCase
    {
        private List<Device> camerasInfo;
        // private Dictionary<string, List<Fps>> pastCameraRecords;
        private CamerasContainer camerasContainer;
        private ChallengeBuffers challengeBuffers;
        private int maxElementsInPastCollection = 10;
        private int maxElementsInFutureCollection = 10;
        private Dictionary<string, string> camerasNames;
        private ChallengeWriter challengeWriter;
        private string pathToVideos = "test";
        List<Fps> fpsList;
        Fps fpsItem;
        Bitmap bitmap;

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            // pastCameraRecords = InitializePastCameraRecords();
            camerasContainer = new CamerasContainer(camerasInfo);
            challengeBuffers = new ChallengeBuffers(camerasContainer, maxElementsInPastCollection,
                                                                        maxElementsInFutureCollection);

            fpsList = new List<Fps>();
            fpsItem = new Fps();
            bitmap = new Bitmap(@"bitmap/bitmap.jpg");

            fpsItem.AddFrame(bitmap);
            fpsList.Add(fpsItem);

            challengeBuffers.AddNewPastCameraRecord("pastKey", fpsList);
            challengeBuffers.AddNewFutureCameraRecord("futureKey", fpsList);

            camerasNames = InitializeCamerasNames();
            challengeWriter = new ChallengeWriter(challengeBuffers, camerasNames, pathToVideos);
        }

        [Test]
        public void WriteVideoWritesVideo()
        {
            // Arrange
            challengeWriter.WriteChallenge();
            // Act

            // Assert
            Assert.True(true);
        }

        private Dictionary<string, string> InitializeCamerasNames()
        {
            Dictionary<string, string> camerasNames = new Dictionary<string, string>();
            camerasNames.Add("cameraOne", "valueOne");

            return camerasNames;
        }

        private Dictionary<string, List<Fps>> InitializePastCameraRecords()
        {
            Dictionary<string, List<Fps>> records = new Dictionary<string, List<Fps>>();
            
            records.Add("CameraOne", fpsList);

            return records;
        }
    }
}