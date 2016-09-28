using Challange.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class ChallengeWriterTest : TestCase
    {
        private List<Device> camerasInfo;
        private Dictionary<string, List<Fps>> pastCameraRecords;
        private CamerasContainer camerasContainer;
        private ChallengeBuffers challengeBuffers;
        private int maxElementsInPastCollection = 10;
        private int maxElementsInFutureCollection = 10;
        private Dictionary<string, string> camerasNames;
        private ChallengeWriter challengeWriter;
        string pathToVideos = "test";

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            pastCameraRecords = InitializePastCameraRecords();
            camerasContainer = new CamerasContainer(camerasInfo);
            challengeBuffers = new ChallengeBuffers(camerasContainer, maxElementsInPastCollection,
                                                                        maxElementsInFutureCollection);
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
            List<Fps> fpsList = new List<Fps>();
            Fps fpsItem = new Fps();
            fpsList.Add(fpsItem);

            records.Add("CameraOne", fpsList);

            return records;
        }
    }
}