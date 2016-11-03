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
using NSubstitute;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class CamerasContainerTests : TestCase
    {
        private PylonCamera camera;
        private CamerasContainer container;
        private List<Device> camerasInfo;
        private string pylonCameraName = "Pylon Camera";

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            camera = Substitute.For<PylonCamera>((uint)1, pylonCameraName);
            container = new CamerasContainer(camerasInfo);
        }

        [Test]
        public void GetCamerasReturnsProperNumber()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(2, container.CamerasNumber);
        }

        [Test]
        public void GetCamerasReturnsProperCountIfCamerasAdded()
        {
            // Arrange

            // Act
            AddCamera(container, camera);
            AddCamera(container, camera);

            // Assert
            Assert.AreEqual(4, container.CamerasNumber);
        }

        [Test]
        public void AddCameraAddsCameraToContainer()
        {
            // Arrange

            // Act
            AddCamera(container, camera);

            // Assert
            Assert.AreEqual(container.GetCameras.ElementAt(2), camera);
        }

        [Test]
        public void RemoveCameraRemovesCameraFromContainer()
        {
            // Arrange

            // Act
            AddCamera(container, camera);
            RemoveCamera(container, camera);

            // Assert
            Assert.AreEqual(2, container.CamerasNumber);
        }

        [Test]
        public void StopAllCamerasTest()
        {
            // Arrange
            AddCamera(container, camera);

            // Act
            container.StopAllCameras();

            // Assert
            camera.Received().Stop();
        }

        [Test]
        public void SetCameraNameTest()
        {
            // Arrange
            string newCameraName = "CameraName";
            AddCamera(container, camera);

            // Act
            container.SetCameraName("Pylon Camera", newCameraName);

            // Assert
            Assert.AreEqual(camera.Name, newCameraName);
        }

        [Test]
        public void SetCameraNameTestTwo()
        {
            // Arrange
            string newCameraName = "CameraName";
            AddCamera(container, camera);

            // Act
            container.SetCameraName("Does Not Exist", newCameraName);

            // Assert
            Assert.AreEqual(camera.Name, "1");
        }

        [Test]
        public void GetCameraByKeyTest()
        {
            // Arrange
            string key = camera.FullName;
            AddCamera(container, camera);

            // Act
            Camera receivedCamera = container.GetCameraByKey(key);

            // Assert
            Assert.AreEqual(camera.Name, receivedCamera.Name);
        }

        private void AddCamera(CamerasContainer container, PylonCamera camera)
        {
            container.AddCamera(camera);
        }

        private void RemoveCamera(CamerasContainer container, PylonCamera camera)
        {
            container.RemoveCamera(camera);
        }
    }
}
