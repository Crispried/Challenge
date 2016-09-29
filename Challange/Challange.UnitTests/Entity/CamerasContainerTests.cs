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
            camera = new PylonCamera(1, pylonCameraName);
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
