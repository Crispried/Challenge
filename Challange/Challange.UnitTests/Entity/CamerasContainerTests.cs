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
            camerasInfo = InitializeCamerasInfo();
            camera = new PylonCamera(1, pylonCameraName);
            camerasContainer = Substitute.For<ICamerasContainer>();
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
