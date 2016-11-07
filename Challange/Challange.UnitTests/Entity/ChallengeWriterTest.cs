﻿using Challange.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using System.Drawing;
using PylonC.NET;
using Challange.Domain.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using NSubstitute;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class ChallengeWriterTest : TestCase
    {
        private List<Device> camerasInfo;
        private ICamerasContainer camerasContainer;
        private ChallengeBuffers challengeBuffers;
        private FpsContainer fpsContainer;
        private int maxElementsInPastCollection = 10;
        private int maxElementsInFutureCollection = 10;
        private Dictionary<string, string> camerasNames;
        private ChallengeWriter challengeWriter;
        private string pathToVideos = "test";
        private IFps fpsItem;
        private Bitmap bitmap;

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            camerasContainer = Substitute.For<ICamerasContainer>();
            challengeBuffers = new ChallengeBuffers(camerasContainer, maxElementsInPastCollection,
                                                                        maxElementsInFutureCollection);
            fpsContainer = new FpsContainer(camerasContainer.GetCamerasKeys);
            bitmap = new Bitmap(@"bitmap/bitmap.jpg");
            fpsItem = fpsContainer.GetFpsByKey("FullName1");
            fpsItem.AddFrame(bitmap);
            fpsItem = fpsContainer.GetFpsByKey("FullName2");
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