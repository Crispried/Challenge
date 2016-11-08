using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PylonC.NETSupportLibrary;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public interface IPylonImageProvider
    {
        void Open(uint cameraIndex);

        void ContinuousShot();

        void Close();

        void Stop();

        ImageProvider.Image GetLatestImage();

        void ReleaseImage();
    }
}
