using Challange.Domain.Entities;
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
        void InitializeCameras(List<Device> deviceList);
        int CamerasNumber { get; }
        List<Camera> GetCameras { get; }
        List<string> GetCamerasKeys { get; }
        List<string> GetCamerasNames { get; }
        void SetCameraName(string key, string cameraFullName);
        string GetCameraNameByKey(string key);
        void AddCamera(Camera camera);
        void RemoveCamera(Camera camera);
        bool IsEmpty();
        void StopAllCameras();
        Camera GetCameraByKey(string key);
    }
}
