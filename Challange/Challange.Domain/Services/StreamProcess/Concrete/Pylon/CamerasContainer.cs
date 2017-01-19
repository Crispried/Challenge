using Challange.Domain.Services.StreamProcess.Abstract;
using System.Collections.Generic;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public class CamerasContainer : ICamerasContainer
    {
        private List<ICamera> _camerasContainer;

        public CamerasContainer()
        {
            _camerasContainer = new List<ICamera>();
        }

        public int CamerasNumber
        {
            get
            {
                return _camerasContainer.Count;
            }
        }

        public List<ICamera> GetCameras
        {
            get
            {
                return _camerasContainer;
            }
        }

        public List<string> GetCamerasKeys()
        {
            List<string> camerasKeys = new List<string>();
            foreach (var camera in _camerasContainer)
            {
                camerasKeys.Add(camera.FullName);
            }
            return camerasKeys;
        }

        public List<string> GetCamerasNames()
        {
            List<string> camerasNames = new List<string>();
            foreach (var camera in _camerasContainer)
            {
                camerasNames.Add(camera.Name);
            }
            return camerasNames;
        }

        public void SetCameraName(string fullName, string newCameraName)
        {
            var camera = _camerasContainer.Find(cam => cam.FullName == fullName);
            if(camera != null)
            {
                camera.Name = newCameraName;
            }
        }

        public void AddCamera(ICamera camera)
        {
            _camerasContainer.Add(camera);
        }

        public void AddCameras(List<ICamera> cameras)
        {
            _camerasContainer.AddRange(cameras);
        }

        public void RemoveCamera(ICamera camera)
        {
            _camerasContainer.Remove(camera);
        }

        public bool IsEmpty()
        {
            return _camerasContainer.Count == 0;
        }

        public ICamera GetCameraByKey(string key)
        {
            return _camerasContainer.Find(camera => camera.FullName == key);
        }
    }
}
