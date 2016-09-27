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
    public class CamerasContainer
    {
        private List<Camera> camerasContainer;
        private List<Device> camerasInfo;

        public CamerasContainer(List<Device> camerasInfo)
        {
            this.camerasInfo = camerasInfo;
            camerasContainer = new List<Camera>();
            InitializeCameras();
        }

        private void InitializeCameras()
        {
            PylonCamera tmpCamera;
            foreach (var cameraInfo in camerasInfo)
            {
                tmpCamera = new PylonCamera(cameraInfo.Index, cameraInfo.FullName);
                AddCamera(tmpCamera);
            }
        }

        public int Count
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

        public List<string> GetCamerasFullNames
        {
            get
            {
                List<string> camerasFullNames = new List<string>();
                foreach (Camera camera in camerasContainer)
                {
                    camerasFullNames.Add(camera.FullName);
                }
                return camerasFullNames;
            }
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
