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
        private Dictionary<string, string> camerasInfo;

        public CamerasContainer(List<Device> deviceList)
        {
            this.deviceList = deviceList;
            camerasContainer = new List<Camera>();
            InitializeCameras();
            InitializeCamerasInfo();
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

        private void InitializeCamerasInfo()
        {
            camerasInfo = new Dictionary<string, string>();
            foreach (Camera camera in camerasContainer)
            {
                camerasInfo.Add(camera.FullName, camera.FullName);
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
                foreach (var key in camerasInfo.Keys)
                {
                    camerasKeys.Add(key);
                }
                return camerasKeys;
            }
        }

        public List<string> GetCamerasFullNames
        {
            get
            {
                List<string> camerasNames = new List<string>();
                foreach (var value in camerasInfo.Values)
                {
                    camerasNames.Add(value);
                }
                return camerasNames;
            }
        }

        public void SetCameraFullName(string key, string cameraFullName)
        {
            string value;
            camerasInfo.TryGetValue(key, out value);
            value = cameraFullName;
        }

        public string GetCameraFullNameByKey(string key)
        {
            string cameraFullName;
            camerasInfo.TryGetValue(key, out cameraFullName);
            return cameraFullName;
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
    }
}
