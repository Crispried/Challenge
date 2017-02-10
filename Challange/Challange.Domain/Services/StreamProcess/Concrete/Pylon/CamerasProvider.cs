using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using System;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public class CamerasProvider : ICamerasProvider
    {
        private ICamerasContainer _camerasContainer;
        private IDevicesProvider _devicesProvider;
        private IEventSubscriber _eventSubscriber;

        public CamerasProvider(ICamerasContainer camerasContainer,
                               IDevicesProvider devicesProvider,
                               IEventSubscriber eventSubscriber)
        {
            _camerasContainer = camerasContainer;
            _devicesProvider = devicesProvider;
            _eventSubscriber = eventSubscriber;
        }

        public ICamerasContainer CamerasContainer
        {
            get
            {
                return _camerasContainer;
            }
        }

        public void InitializeCameras()
        {
            var cameras = _devicesProvider.GetConnectedCameras();
            _camerasContainer.AddCameras(cameras);
        }

        public void StopAllCameras()
        {
            foreach (var camera in _camerasContainer.GetCameras)
            {
                camera.Stop();
            }
        }

        public void StartAllCameras(Action<object, EventArgs> cameraEventHandler)
        {
            foreach (var camera in _camerasContainer.GetCameras)
            {
                _eventSubscriber.AddEventHandler(camera, "NewFrameEvent", cameraEventHandler);
                camera.Start();
            }
        }

        public void StartCamera(ICamera camera, Action<object, EventArgs> cameraEventHandler)
        {
            _eventSubscriber.AddEventHandler(camera, "NewFrameEvent", cameraEventHandler);
            camera.Start();
        }

        public void StopCamera(ICamera camera)
        {
            camera.Stop();
        }
    }
}
