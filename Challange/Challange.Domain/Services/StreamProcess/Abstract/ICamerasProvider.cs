using Challange.Domain.Services.Event.Abstract;
using System;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public interface ICamerasProvider
    {
        void InitializeCameras();

        void StopAllCameras();

        void StartAllCameras(Action<object, EventArgs> cameraEventHandler, IEventSubscriber eventSubscriber);
    }
}
