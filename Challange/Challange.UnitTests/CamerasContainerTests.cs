using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Domain.Services.StreamProcess.Concrete;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using Challange.Domain.Entities;

namespace Challange.UnitTests
{
    class CamerasContainerTests
    {
        private PylonCamera camera;

        private CamerasContainer<PylonCamera> container;

        [SetUp]
        public void SetUp()
        {
            camera = new PylonCamera(1, "Pylon Camera");
            container = new CamerasContainer<PylonCamera>();
        }

        [Test]
        public void GetCamerasReturnsZeroIfNoCamerasPresented()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(0, container.GetCameras.Count);
        }

        [Test]
        public void GetCamerasReturnsProperCountIfCamerasAdded()
        {
            // Arrange

            // Act
            AddCamera(container, camera);
            AddCamera(container, camera);

            // Assert
            Assert.AreEqual(2, container.GetCameras.Count);
        }

        [Test]
        public void AddCameraAddsCameraToContainer()
        {
            // Arrange

            // Act
            AddCamera(container, camera);

            // Assert
            Assert.AreEqual(1, container.GetCameras.Count);
        }

        [Test]
        public void RemoveCameraRemovesCameraFromContainer()
        {
            // Arrange

            // Act
            AddCamera(container, camera);
            RemoveCamera(container, camera);

            // Assert
            Assert.AreEqual(0, container.GetCameras.Count);
        }

        private void AddCamera(CamerasContainer<PylonCamera> container, PylonCamera camera)
        {
            container.AddCamera(camera);
        }

        private void RemoveCamera(CamerasContainer<PylonCamera> container, PylonCamera camera)
        {
            container.RemoveCamera(camera);
        }
    }
}
