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
using Challange.Domain.Services.Event;

namespace Challange.UnitTests.Services.StreamProcess
{
    [TestFixture]
    class CamerasContainerTests : TestCase
    {
        private Camera camera;
        private ICamerasContainer camerasContainer;
        private List<Device> camerasInfo;
        private ICameraProvider cameraProvider;
        private string pylonCameraName = "Pylon Camera";

        [SetUp]
        public void SetUp()
        {
            camerasInfo = InitializeCamerasInfo();
            cameraProvider = Substitute.For<ICameraProvider>();
            camerasContainer = new CamerasContainer(cameraProvider);
            camera = Substitute.For<Camera>((uint)1, pylonCameraName);
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
        public void GetCamerasKeysPropety()
        {
            // Arrange
            var camerasContainerMock = Substitute.For<ICamerasContainer>();
            // Act
            var camerasKeys = camerasContainerMock.GetCamerasKeys;
            // Assert
            var test = camerasContainerMock.Received().GetCamerasKeys;
        }

        [Test]
        public void GetCamerasKeysReturnCorrectValue()
        {
            // Arrange
            var camera = new Camera(1, "name");
            AddCamera(camerasContainer, camera);
            // Act
            var camerasKeys = camerasContainer.GetCamerasKeys;
            // Assert
            Assert.True(camerasKeys.Contains("name"));
        }

        [Test]
        public void GetCamerasNamesProperty()
        {
            // Arrange
            var camerasContainerMock = Substitute.For<ICamerasContainer>();
            // Act
            var camerasNames = camerasContainerMock.GetCamerasNames;
            // Assert
            var test = camerasContainerMock.Received().GetCamerasNames;
        }

        [Test]
        public void GetCamerasNamesReturnsCorrectValue()
        {
            // Arrange
            var camera = new Camera(1, "name");
            AddCamera(camerasContainer, camera);
            // Act
            var camerasNames = camerasContainer.GetCamerasNames;
            // Assert
            Assert.True(camerasNames.Contains("1"));
        }

        [Test]
        public void GetCamerasNamesAsQueueProperty()
        {
            // Arrange
            var camerasContainerMock = Substitute.For<ICamerasContainer>();
            // Act
            var camerasNames = camerasContainerMock.GetCamerasNamesAsQueue;
            // Assert
            var test = camerasContainerMock.Received().GetCamerasNamesAsQueue;
        }

        [Test]
        public void GetCamerasNamesAsQueueReturnsCorrectValue()
        {
            // Arrange
            var camera = new Camera(1, "name");
            AddCamera(camerasContainer, camera);
            // Act
            var camerasNames = camerasContainer.GetCamerasNamesAsQueue;
            // Assert
            Assert.True(camerasNames.Contains("1"));
        }

        [Test]
        public void InitializeCamerasTest()
        {
            // Arrange
            // Act
            camerasContainer.InitializeCameras();
            // Assert
            var cameras = cameraProvider.Received().GetConnectedCameras();
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
        public void StartAllCamerasTest()
        {
            // Arrange
            AddCamera(camerasContainer, camera);
            var handler = Substitute.For<Action<object, EventArgs>>();
            var eventSubscriber = Substitute.For<IEventSubscriber>();

            // Act
            camerasContainer.StartAllCameras(handler, eventSubscriber);

            // Assert
            eventSubscriber.Received().AddEventHandler(camera, "NewFrameEvent", handler);
            camera.Received().Start();
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
        public void GetCameraByKeyTest()
        {
            // Arrange
            string key = camera.FullName;
            AddCamera(camerasContainer, camera);

            // Act
            ICamera receivedCamera = camerasContainer.GetCameraByKey(key);

            // Assert
            Assert.AreEqual(camera.Name, receivedCamera.Name);
        }

        private void AddCamera(ICamerasContainer container, Camera camera)
        {
            container.AddCamera(camera);
        }

        private void RemoveCamera(ICamerasContainer container, Camera camera)
        {
            container.RemoveCamera(camera);
        }
    }
}
