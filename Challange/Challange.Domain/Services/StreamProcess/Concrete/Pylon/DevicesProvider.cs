using Challange.Domain.Services.StreamProcess.Abstract;
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
    public class DevicesProvider : IDevicesProvider
    {
        public List<ICamera> GetConnectedCameras()
        {
            try
            {
                var devices = EnumerateDevices();
                var cameras = new List<ICamera>();
                foreach (var device in devices)
                {
                    var camera = new Camera(device.Index, device.FullName);
                    cameras.Add(camera);
                }
                return cameras;
            }
            catch
            {
                return new List<ICamera>();
            }
        }
    }
}
