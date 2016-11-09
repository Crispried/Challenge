using Challange.Domain.Services.StreamProcess.Abstract;
using PylonC.NETSupportLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    [ExcludeFromCodeCoverage]
    public class PylonCameraProvider : ICameraProvider
    {
        public List<Camera> GetConnectedCameras()
        {
            List<Device> devices = null;
            List<Camera> cameras = new List<Camera>();
            try
            {
                devices = EnumerateDevices();
                foreach (var device in devices)
                {
                    var camera = new Camera(device.Index, device.FullName);
                    cameras.Add(camera);
                }
            }
            catch
            {
            }
            return cameras;
        }
    }
}
