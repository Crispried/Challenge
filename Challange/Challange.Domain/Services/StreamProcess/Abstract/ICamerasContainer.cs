using Challange.Domain.Entities;
using Challange.Domain.Services.Event;
using Challange.Domain.Services.StreamProcess.Concrete;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
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
        int CamerasNumber { get; }

        List<ICamera> GetCameras { get; }

        List<string> GetCamerasKeys();

        List<string> GetCamerasNames();

        void SetCameraName(string fullName, string newCameraName);

        void AddCameras(List<ICamera> cameras);

        void AddCamera(ICamera camera);

        void RemoveCamera(ICamera camera);

        bool IsEmpty();  

        ICamera GetCameraByKey(string key);
    }
}
