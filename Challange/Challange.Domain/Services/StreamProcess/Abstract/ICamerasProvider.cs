using Challange.Domain.Services.Event.Abstract;
using System;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public interface ICamerasProvider
    {
        ICamerasContainer CamerasContainer { get; }

        void InitializeCameras();

        void StopAllCameras();

        void StartAllCameras(Action<object, EventArgs> cameraEventHandler);

        void StartCamera(ICamera camera, Action<object, EventArgs> cameraEventHandler);

        void StopCamera(ICamera camera);
    }
}
