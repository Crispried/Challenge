using Challange.Domain.Entities;
using Challange.Domain.Services.StreamProcess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public interface ICamerasContainer
    {
        void InitializeCameras();

        int CamerasNumber { get; }

        List<ICamera> GetCameras { get; }

        List<string> GetCamerasKeys { get; }

        List<string> GetCamerasNames { get; }

        void SetCameraName(string key, string cameraFullName);

        void AddCamera(ICamera camera);

        void RemoveCamera(ICamera camera);

        bool IsEmpty();

        void StopAllCameras();

        ICamera GetCameraByKey(string key);
    }
}
