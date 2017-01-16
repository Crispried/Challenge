using Challange.Domain.Services.Event;
using Challange.Domain.Services.StreamProcess.Abstract;
using PylonC.NETSupportLibrary;
using System;
using System.Collections.Generic;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public class CamerasProvider : ICamerasProvider
    {
        private ICamerasContainer _camerasContainer;
        private IDevicesProvider _devicesProvider;

        public CamerasProvider(ICamerasContainer camerasContainer,
                               IDevicesProvider devicesProvider)
        {
            _camerasContainer = camerasContainer;
            _devicesProvider = devicesProvider;
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

        public void StartAllCameras(Action<object, EventArgs> cameraEventHandler, IEventSubscriber eventSubscriber)
        {
            foreach (var camera in _camerasContainer.GetCameras)
            {
                eventSubscriber.AddEventHandler(camera, "NewFrameEvent", cameraEventHandler);
                camera.Start();
            }
        }
    }
}
