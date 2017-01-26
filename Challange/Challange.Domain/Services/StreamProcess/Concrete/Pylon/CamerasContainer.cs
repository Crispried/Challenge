using Challange.Domain.Services.StreamProcess.Abstract;
using System.Collections.Generic;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public class CamerasContainer : ICamerasContainer
    {
        private List<ICamera> cameras;

        public CamerasContainer()
        {
            cameras = new List<ICamera>();
        }

        public int CamerasNumber
        {
            get
            {
                return cameras.Count;
            }
        }

        public List<ICamera> GetCameras
        {
            get
            {
                return cameras;
            }
        }

        public List<string> GetCamerasKeys()
        {
            List<string> camerasKeys = new List<string>();
            foreach (var camera in cameras)
            {
                camerasKeys.Add(camera.FullName);
            }
            return camerasKeys;
        }

        public List<string> GetCamerasNames()
        {
            List<string> camerasNames = new List<string>();
            foreach (var camera in cameras)
            {
                camerasNames.Add(camera.Name);
            }
            return camerasNames;
        }

        public void SetCameraName(string fullName, string newCameraName)
        {
            var camera = cameras.Find(cam => cam.FullName == fullName);
            if(camera != null)
            {
                camera.Name = newCameraName;
            }
        }

        public void AddCamera(ICamera camera)
        {
            cameras.Add(camera);
        }

        public void AddCameras(List<ICamera> cameras)
        {
            this.cameras.AddRange(cameras);
        }

        public void RemoveCamera(ICamera camera)
        {
            cameras.Remove(camera);
        }

        public bool IsEmpty()
        {
            return cameras.Count == 0;
        }

        public ICamera GetCameraByKey(string key)
        {
            return cameras.Find(camera => camera.FullName == key);
        }
    }
}
