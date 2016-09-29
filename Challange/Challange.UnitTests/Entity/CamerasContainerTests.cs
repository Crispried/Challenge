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
        public void GetCamerasReturnsOneIfNoCamerasPresented()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(1, container.GetCameras.Count);
        }

        [Test]
        public void GetCamerasReturnsProperCountIfCamerasAdded()
        {
            // Arrange

            // Act
            AddCamera(container, camera);
            AddCamera(container, camera);

            // Assert
            Assert.AreEqual(3, container.GetCameras.Count);
        }

        [Test]
        public void AddCameraAddsCameraToContainer()
        {
            // Arrange

            // Act
            AddCamera(container, camera);

            // Assert
            Assert.AreEqual(2, container.GetCameras.Count);
        }

        [Test]
        public void RemoveCameraRemovesCameraFromContainer()
        {
            // Arrange

            // Act
            AddCamera(container, camera);
            RemoveCamera(container, camera);

            // Assert
            Assert.AreEqual(1, container.GetCameras.Count);
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
