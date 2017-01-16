using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Services.StreamProcess.Abstract;
using NSubstitute;
using Challange.Domain.Services.Event;

namespace Challange.UnitTests.Services.StreamProcess
{
    [TestFixture]
    class CamerasContainerTests : TestCase
    {
        private Camera _camera;
        private ICamerasContainer _camerasContainer;

        [SetUp]
        public void SetUp()
        {
            _camerasContainer = new CamerasContainer();
            _camera = Substitute.For<Camera>((uint)1, "Pylon Camera");
        }

        [Test]
        public void GetCamerasNumberProperty()
        {
            // Arrange
            var camerasContainerMock = Substitute.For<ICamerasContainer>();
            // Act
            var number = camerasContainerMock.CamerasNumber;
            // Assert
            var test = camerasContainerMock.Received().CamerasNumber;
        }

        [Test]
        public void GetCamerasProperty()
        {
            // Arrange
            var camerasContainerMock = Substitute.For<ICamerasContainer>();
            // Act
            var cameras = camerasContainerMock.GetCameras;
            // Assert
            var test = camerasContainerMock.Received().GetCameras;
        }

        [Test]
        public void GetCamerasKeysReturnCorrectValue()
        {
            // Arrange
            var camera = new Camera(1, "name");
            AddCamera(_camerasContainer, camera);
            // Act
            var camerasKeys = _camerasContainer.GetCamerasKeys();
            // Assert
            Assert.True(camerasKeys.Contains("name"));
        }

        [Test]
        public void GetCamerasNamesReturnsCorrectValue()
        {
            // Arrange
            var camera = new Camera(1, "name");
            AddCamera(_camerasContainer, camera);
            // Act
            var camerasNames = _camerasContainer.GetCamerasNames();
            // Assert
            Assert.True(camerasNames.Contains("1"));
        }

        [Test]
        public void SetCameraNameTest()
        {
            // Arrange
            string newCameraName = "CameraName";
            AddCamera(_camerasContainer, _camera);
            // Act
            _camerasContainer.SetCameraName("Pylon Camera", newCameraName);
            // Assert
            Assert.AreEqual(_camera.Name, newCameraName);
        }

        [Test]
        public void AddCameraAddsCameraToContainer()
        {
            // Arrange

            // Act
            AddCamera(_camerasContainer, _camera);

            // Assert
            Assert.AreEqual(_camerasContainer.GetCameras.ElementAt(0), _camera);
        }

        [Test]
        public void AddCamerasAddsCamerasToContainer()
        {
            // Arrange
            List<ICamera> cameras = new List<ICamera>() { _camera };
            // Act
            AddCameras(_camerasContainer, cameras);

            // Assert
            Assert.AreEqual(_camerasContainer.GetCameras.ElementAt(0), cameras[0]);
        }

        [Test]
        public void RemoveCameraRemovesCameraFromContainer()
        {
            // Arrange

            // Act
            AddCamera(_camerasContainer, _camera);
            RemoveCamera(_camerasContainer, _camera);

            // Assert
            Assert.AreEqual(0, _camerasContainer.CamerasNumber);
        }

        [Test]
        public void GetCamerasReturnsProperNumber()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(0, _camerasContainer.CamerasNumber);
        }

        [Test]
        public void GetCamerasReturnsProperCountIfCamerasAdded()
        {
            // Arrange

            // Act
            AddCamera(_camerasContainer, _camera);
            AddCamera(_camerasContainer, _camera);

            // Assert
            Assert.AreEqual(2, _camerasContainer.CamerasNumber);
        }

        [Test]
        public void IsEmptyWithNoCamerasTest()
        {
            // Arrange

            // Act
            bool isEmpty = _camerasContainer.IsEmpty();

            // Assert
            Assert.True(isEmpty);
        }

        [Test]
        public void IsEmptyWithOneCameraTest()
        {
            // Arrange
            AddCamera(_camerasContainer, _camera);

            // Act
            bool isEmpty = _camerasContainer.IsEmpty();

            // Assert
            Assert.False(isEmpty);
        }

        [Test]
        public void GetCameraByKeyTest()
        {
            // Arrange
            string key = _camera.FullName;
            AddCamera(_camerasContainer, _camera);

            // Act
            ICamera receivedCamera = _camerasContainer.GetCameraByKey(key);

            // Assert
            Assert.AreEqual(_camera.Name, receivedCamera.Name);
        }

        private void AddCamera(ICamerasContainer container, ICamera camera)
        {
            container.AddCamera(camera);
        }

        private void AddCameras(ICamerasContainer container, List<ICamera> cameras)
        {
            container.AddCameras(cameras);
        }

        private void RemoveCamera(ICamerasContainer container, ICamera camera)
        {
            container.RemoveCamera(camera);
        }
    }
}
