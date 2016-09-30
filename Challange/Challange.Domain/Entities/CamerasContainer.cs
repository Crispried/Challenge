using Challange.Domain.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Domain.Entities
{
    public class CamerasContainer : ICamerasContainer
    {
        private List<Camera> camerasContainer;
        private List<Device> deviceList;

        public CamerasContainer(List<Device> deviceList)
        {
            this.deviceList = deviceList;
            camerasContainer = new List<Camera>();
            InitializeCameras();
        }

        private void InitializeCameras()
        {
            PylonCamera tmpCamera;
            foreach (var cameraInfo in deviceList)
            {
                tmpCamera = new PylonCamera(cameraInfo.Index, cameraInfo.FullName);
                AddCamera(tmpCamera);
            }
        }

        public int CamerasNumber
        {
            get
            {
                return camerasContainer.Count;
            }
        }

        public List<Camera> GetCameras
        {
            get
            {
                return camerasContainer;
            }
        }

        public List<string> GetCamerasKeys
        {
            get
            {
                List<string> camerasKeys = new List<string>();
                foreach (var camera in camerasContainer)
                {
                    camerasKeys.Add(camera.FullName);
                }
                return camerasKeys;
            }
        }

        public List<string> GetCamerasNames
        {
            get
            {
                List<string> camerasNames = new List<string>();
                foreach (var camera in camerasContainer)
                {
                    camerasNames.Add(camera.Name);
                }
                return camerasNames;
            }
        }

        public void SetCameraName(string fullName, string cameraName)
        {
            var camera = camerasContainer.Find(cam => cam.FullName == fullName);
            if(camera != null)
            {
                camera.Name = cameraName;
            }
        }

        public string GetCameraNameByKey(string fullName)
        {
            var camera = camerasContainer.Find(cam => cam.FullName == fullName);
            return camera.Name;
        }

        public void AddCamera(Camera camera)
        {
            camerasContainer.Add(camera);
        }

        public void RemoveCamera(Camera camera)
        {
            camerasContainer.Remove(camera);
        }

        public bool IsEmpty()
        {
            return camerasContainer.Count == 0;
        }

        public void StopAllCameras()
        {
            foreach (Camera camera in camerasContainer)
            {
                camera.Stop();
            }
        }
    }
}
