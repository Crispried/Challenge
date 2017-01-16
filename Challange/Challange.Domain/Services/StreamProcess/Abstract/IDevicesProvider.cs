using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public interface IDevicesProvider
    {
        List<ICamera> GetConnectedCameras();
    }
}
