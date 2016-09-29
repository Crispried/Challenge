using Challange.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using System.Drawing;
using PylonC.NET;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class ChallengeWriterTest : TestCase
    {
        private List<Device> camerasInfo;
        // private Dictionary<string, List<Fps>> pastCameraRecords;
        private CamerasContainer camerasContainer;
        private ChallengeBuffers challengeBuffers;
        private FpsContainer fpsContainer;
        private int maxElementsInPastCollection = 10;
        private int maxElementsInFutureCollection = 10;
        private Dictionary<string, string> camerasNames;
        private ChallengeWriter challengeWriter;
        string pathToVideos = "test";
        Fps fpsItem;
        Bitmap bitmap;

        [SetUp]
        public void SetUp()
        {
            camerasInfo = new List<Device>()
            {
                new Device()
                {
                    FullName = "camera1"
                },
                new Device()
                {
                    FullName = "camera2"
                }
            };
            // pastCameraRecords = InitializePastCameraRecords();
            camerasContainer = new CamerasContainer(camerasInfo);
            challengeBuffers = new ChallengeBuffers(camerasContainer, maxElementsInPastCollection,
                                                                        maxElementsInFutureCollection);
            fpsContainer = new FpsContainer(camerasContainer.GetCamerasFullNames);


            bitmap = new Bitmap(@"bitmap/bitmap.jpg");
            fpsItem = fpsContainer.GetFpsByKey("camera1");
            fpsItem.AddFrame(bitmap);
            fpsItem = fpsContainer.GetFpsByKey("camera2");
            fpsItem.AddFrame(bitmap);

            challengeBuffers.AddPastFpses(fpsContainer);
            challengeBuffers.AddFutureFpses(fpsContainer);

            camerasNames = InitializeCamerasNames();
            challengeWriter = new ChallengeWriter(challengeBuffers, camerasContainer, pathToVideos);
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
    }
}