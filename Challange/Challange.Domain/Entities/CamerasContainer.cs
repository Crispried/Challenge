using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public class CamerasContainer
    {
        private List<Camera> camerasContainer;

        public CamerasContainer()
        {
            camerasContainer = new List<Camera>();
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
