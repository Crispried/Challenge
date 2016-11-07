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
<<<<<<< HEAD
using Challange.Domain.Services.StreamProcess.Abstract;
=======
>>>>>>> 8bee6a75e986a196494ee02467e98ffe6c0f24f9
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
            camerasInfo = InitializeCamerasInfo();
<<<<<<< HEAD
            camera = new PylonCamera(1, pylonCameraName);
            camerasContainer = Substitute.For<ICamerasContainer>();
=======
            camera = Substitute.For<PylonCamera>((uint)1, pylonCameraName);
            container = new CamerasContainer(camerasInfo);
>>>>>>> 8bee6a75e986a196494ee02467e98ffe6c0f24f9
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

<<<<<<< HEAD
        private void AddCamera(ICamerasContainer container, PylonCamera camera)
=======
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
>>>>>>> 8bee6a75e986a196494ee02467e98ffe6c0f24f9
        {
            container.AddCamera(camera);
        }

        private void RemoveCamera(ICamerasContainer container, PylonCamera camera)
        {
            container.RemoveCamera(camera);
        }
    }
}
