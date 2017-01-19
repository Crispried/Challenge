using System.Collections.Generic;

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
