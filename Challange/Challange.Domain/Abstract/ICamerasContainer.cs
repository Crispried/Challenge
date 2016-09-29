using Challange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Abstract
{
    public interface ICamerasContainer
    {
        int CamerasNumber { get; }
        List<Camera> GetCameras { get; }
        List<string> GetCamerasKeys { get; }
        List<string> GetCamerasFullNames { get; }
        void SetCameraFullName(string key, string cameraFullName);
        string GetCameraFullNameByKey(string key);
        void AddCamera(Camera camera);
        void RemoveCamera(Camera camera);
        bool IsEmpty();
    }
}
