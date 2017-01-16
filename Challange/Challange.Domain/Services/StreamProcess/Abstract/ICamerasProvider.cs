using Challange.Domain.Services.Event;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public interface ICamerasProvider
    {
        void InitializeCameras();

        void StopAllCameras();

        void StartAllCameras(Action<object, EventArgs> cameraEventHandler, IEventSubscriber eventSubscriber);
    }
}
