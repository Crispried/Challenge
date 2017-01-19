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
        private ICamerasProvider _cameraProvider;
        private ICamerasContainer _camerasContainer;
        private IDevicesProvider _devicesProvider;
        private ICamera _camera;

        [SetUp]
        public void SetUp()
        {
            _camerasContainer = Substitute.For<ICamerasContainer>();
            _devicesProvider = Substitute.For<IDevicesProvider>();
            _cameraProvider = new CamerasProvider(_camerasContainer, _devicesProvider);
            _camera = Substitute.For<ICamera>();
        }

        [Test]
        public void InitializeCamerasTest()
        {
            // Arrange
            var cameras = new List<ICamera>() { _camera };
            _devicesProvider.GetConnectedCameras().Returns(cameras);
            // Act
            _cameraProvider.InitializeCameras();
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
            var eventSubscriber = Substitute.For<IEventSubscriber>();

            // Act
            _cameraProvider.StartAllCameras(handler, eventSubscriber);

            // Assert
            var a = _camerasContainer.Received().GetCameras;
            eventSubscriber.Received().AddEventHandler(cameras[0], "NewFrameEvent", handler);
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
            _cameraProvider.StopAllCameras();

            // Assert
            var a = _camerasContainer.Received().GetCameras;
            cameras[0].Received().Stop();
        }
    }
}
