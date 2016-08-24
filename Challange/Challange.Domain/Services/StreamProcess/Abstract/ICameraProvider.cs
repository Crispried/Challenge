using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PylonC.NETSupportLibrary.DeviceEnumerator;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public interface ICameraProvider
    {
        /// <summary>
        /// returns all names of connected cameras
        /// </summary>
        /// <returns>List of type Devuce</returns>
        List<Device> GetConnectedCameras();
    }
}
