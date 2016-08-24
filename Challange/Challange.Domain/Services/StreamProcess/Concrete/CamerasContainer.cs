using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.StreamProcess.Concrete
{
    public class CamerasContainer<Camera> 
    {
        private List<Camera> camerasContainer;

        public CamerasContainer()
        {
            camerasContainer = new List<Camera>();
        }

        public List<Camera> GetCameras
        {
            get
            {
                return camerasContainer;
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
    }
}
