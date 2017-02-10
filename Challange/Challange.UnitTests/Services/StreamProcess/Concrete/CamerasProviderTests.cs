using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Challange.UnitTests.Services.StreamProcess.Concrete
{
    [TestFixture]
    class CamerasProviderTests : TestCase
    {
        private ICamerasProvider _camerasProvider;
        private ICamerasContainer _camerasContainer;
        private IDevicesProvider _devicesProvider;
        private IEventSubscriber _eventSubscriber;
        private ICamera _camera;

        [SetUp]
        public void SetUp()
        {
            _camerasContainer = Substitute.For<ICamerasContainer>();
            _devicesProvider = Substitute.For<IDevicesProvider>();
            _eventSubscriber = Substitute.For<IEventSubscriber>();
            _camerasProvider = new CamerasProvider(_camerasContainer, _devicesProvider,
                                                  _eventSubscriber);
            _camera = Substitute.For<ICamera>();
        }

        [Test]
        public void GetCamerasContainerProperty()
        {
            // Arrange
            // Act
            var camerasContainer = _camerasProvider.CamerasContainer;
            // Assert
            Assert.IsTrue(camerasContainer == _camerasContainer);
        }

        [Test]
        public void InitializeCamerasTest()
        {
            // Arrange
            var cameras = new List<ICamera>() { _camera };
            _devicesProvider.GetConnectedCameras().Returns(cameras);
            // Act
            _camerasProvider.InitializeCameras();
            // Assert
            _devicesProvider.Received().GetConnectedCameras();
            _camerasContainer.Received().AddCameras(cameras);
        }

        [Test]
        public void StartAllCamerasTest()
        {
            // Arrange
            var cameras = Substitute.For<List<ICamera>>();
            cameras.Add(_camera);
            _camerasContainer.GetCameras.Returns(cameras);
            var handler = Substitute.For<Action<object, EventArgs>>();
            // Act
            _camerasProvider.StartAllCameras(handler);
            // Assert
            var a = _camerasContainer.Received().GetCameras;
            _eventSubscriber.Received().AddEventHandler(cameras[0], "NewFrameEvent", handler);
            cameras[0].Received().Start();
        }

        [Test]
        public void StopAllCamerasTest()
        {
            // Arrange
            var cameras = Substitute.For<List<ICamera>>();
            cameras.Add(_camera);
            _camerasContainer.GetCameras.Returns(cameras);

            // Act
            _camerasProvider.StopAllCameras();

            // Assert
            var a = _camerasContainer.Received().GetCameras;
            cameras[0].Received().Stop();
        }

        [Test]
        public void StartCameraTest()
        {
            // Arrange
            var camera = Substitute.For<ICamera>();
            var handler = Substitute.For<Action<object, EventArgs>>();
              // Act
              _camerasProvider.StartCamera(camera, handler);
            // Assert
            _eventSubscriber.Received().AddEventHandler(camera, "NewFrameEvent", handler);
            camera.Received().Start();
        }

        [Test]
        public void StopCameraTest()
        {
            // Arrange
            var camera = Substitute.For<ICamera>();
            // Act
            _camerasProvider.StopCamera(camera);
            // Assert
            camera.Received().Stop();
        }
    }
}
