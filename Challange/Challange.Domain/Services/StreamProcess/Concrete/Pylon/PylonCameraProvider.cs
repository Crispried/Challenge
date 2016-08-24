using Challange.Domain.Services.StreamProcess.Abstract;
using PylonC.NETSupportLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public class PylonCameraProvider : ICameraProvider
    {
        public List<string> GetConnectedCameras()
        {
            /* Ask the device enumerator for a list of devices. */
            List<DeviceEnumerator.Device> devices =
                    DeviceEnumerator.EnumerateDevices();
            List<string> result = new List<string>();
            /* Add each new device to the list. */
            foreach (DeviceEnumerator.Device device in devices)
            {
                result.Add(device.FullName);
            }
            return result;
        }
    }
}
