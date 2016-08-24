using Challange.Domain.Services.StreamProcess.Abstract;
using PylonC.NETSupportLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public class PylonCameraProvider : ICameraProvider
    {
        public List<Device> GetConnectedCameras()
        {
            return EnumerateDevices();
        }
    }
}
