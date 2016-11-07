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
        private string pylonCameraName = "Pylon Camera";

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();;
            camerasContainer = Substitute.For<ICamerasContainer>();
            camera = Substitute.For<PylonCamera>((uint)1, pylonCameraName);
        }

        [Test]
        public void GetCamerasReturnsProperNumber()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(2, camerasContainer.CamerasNumber);
        }

        [Test]
        public void GetCamerasReturnsProperCountIfCamerasAdded()
        {
            // Arrange

            // Act
            AddCamera(camerasContainer, camera);
            AddCamera(camerasContainer, camera);

            // Assert
            Assert.AreEqual(4, camerasContainer.CamerasNumber);
        }

        [Test]
        public void AddCameraAddsCameraToContainer()
        {
            // Arrange

            // Act
            AddCamera(camerasContainer, camera);

            // Assert
            Assert.AreEqual(camerasContainer.GetCameras.ElementAt(2), camera);
        }

        [Test]
        public void RemoveCameraRemovesCameraFromContainer()
        {
            // Arrange

            // Act
            AddCamera(camerasContainer, camera);
            RemoveCamera(camerasContainer, camera);

            // Assert
            Assert.AreEqual(2, camerasContainer.CamerasNumber);
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
