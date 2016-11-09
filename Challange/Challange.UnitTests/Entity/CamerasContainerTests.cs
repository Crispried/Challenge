using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.StreamProcess.Concrete;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using Challange.Domain.Entities;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Services.StreamProcess.Abstract;
using NSubstitute;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class CamerasContainerTests : TestCase
    {
        private PylonCamera camera;
        private ICamerasContainer camerasContainer;
        private List<Device> camerasInfo;
        private ICameraProvider cameraProvider;
        private string pylonCameraName = "Pylon Camera";

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            cameraProvider = Substitute.For<ICameraProvider>();
            camerasContainer = Substitute.For<ICamerasContainer>();
            camera = Substitute.For<PylonCamera>((uint)1, pylonCameraName);
        }

        [Test]
        public void GetCamerasReturnsProperNumber()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(0, camerasContainer.CamerasNumber);
        }

        [Test]
        public void GetCamerasReturnsProperCountIfCamerasAdded()
        {
            // Arrange

            // Act
            AddCamera(camerasContainer, camera);
            AddCamera(camerasContainer, camera);

            // Assert
            Assert.AreEqual(2, camerasContainer.CamerasNumber);
        }

        [Test]
        public void AddCameraAddsCameraToContainer()
        {
            // Arrange

            // Act
            AddCamera(camerasContainer, camera);

            // Assert
            Assert.AreEqual(camerasContainer.GetCameras.ElementAt(0), camera);
        }

        [Test]
        public void RemoveCameraRemovesCameraFromContainer()
        {
            // Arrange

            // Act
            AddCamera(camerasContainer, camera);
            RemoveCamera(camerasContainer, camera);

            // Assert
            Assert.AreEqual(0, camerasContainer.CamerasNumber);
        }

        [Test]
        public void StopAllCamerasTest()
        {
            // Arrange
            AddCamera(camerasContainer, camera);

            // Act
            camerasContainer.StopAllCameras();

            // Assert
            camera.Received().Stop();
        }

        [Test]
        public void SetCameraNameTest()
        {
            // Arrange
            string newCameraName = "CameraName";
            AddCamera(camerasContainer, camera);

            // Act
            camerasContainer.SetCameraName("Pylon Camera", newCameraName);

            // Assert
            Assert.AreEqual(camera.Name, newCameraName);
        }

        [Test]
        public void SetCameraNameTestTwo()
        {
            // Arrange
            string newCameraName = "CameraName";
            AddCamera(camerasContainer, camera);

            // Act
            camerasContainer.SetCameraName("Does Not Exist", newCameraName);

            // Assert
            Assert.AreEqual(camera.Name, "1");
        }

        [Test]
        public void GetCameraByKeyTest()
        {
            // Arrange
            string key = camera.FullName;
            AddCamera(camerasContainer, camera);

            // Act
            Camera receivedCamera = camerasContainer.GetCameraByKey(key);

            // Assert
            Assert.AreEqual(camera.Name, receivedCamera.Name);
        }

        [Test]
        public void GetCamerasKeysIsEmptyIfNoCamerasProvidedTest()
        {
            // Arrange

            // Act
            List<string> camerasKeys = camerasContainer.GetCamerasKeys;

            // Assert
            Assert.IsEmpty(camerasKeys);
        }

        [Test]
        public void GetCamerasKeysIsNotEmptyIfOneCameraIsProvidedTest()
        {
            // Arrange
            AddCamera(camerasContainer, camera);

            // Act
            List<string> camerasKeys = camerasContainer.GetCamerasKeys;

            // Assert
            Assert.AreEqual(1, camerasKeys.Count);
        }

        [Test]
        public void GetCamerasNamesIsEmptyIfNoCamerasProvidedTest()
        {
            // Arrange

            // Act
            List<string> camerasNames = camerasContainer.GetCamerasNames;

            // Assert
            Assert.IsEmpty(camerasNames);
        }

        [Test]
        public void GetCamerasNamesIsNotEmptyIfOneCameraIsProvidedTest()
        {
            // Arrange
            AddCamera(camerasContainer, camera);

            // Act
            List<string> camerasNames = camerasContainer.GetCamerasNames;

            // Assert
            Assert.AreEqual(1, camerasNames.Count);
        }

        [Test]
        public void IsEmptyWithNoCamerasTest()
        {
            // Arrange

            // Act
            bool isEmpty = camerasContainer.IsEmpty();

            // Assert
            Assert.True(isEmpty);
        }

        [Test]
        public void IsEmptyWithOneCameraTest()
        {
            // Arrange
            AddCamera(camerasContainer, camera);

            // Act
            bool isEmpty = camerasContainer.IsEmpty();

            // Assert
            Assert.False(isEmpty);
        }

        [Test]
        public void InitializeCamerasTest()
        {
            // Arrange
            Device item = new Device();
            item.Index = 1;
            item.FullName = "Name";

            List<Device> deviceList = new List<Device>();
            deviceList.Add(item);

            // Act
            camerasContainer.InitializeCameras(deviceList);

            // Assert
            Assert.AreEqual(1, camerasContainer.CamerasNumber);
        }

        private void AddCamera(ICamerasContainer container, PylonCamera camera)
        {
            container.AddCamera(camera);
        }

        private void RemoveCamera(ICamerasContainer container, PylonCamera camera)
        {
            container.RemoveCamera(camera);
        }
    }
}
