using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PylonC.NETSupportLibrary;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public class PylonImageProvider : IPylonImageProvider
    {
        private ImageProvider imageProvider;

        public PylonImageProvider()
        {
            imageProvider = new ImageProvider();
        }

        public void Open(uint cameraIndex)
        {
            imageProvider.Open(cameraIndex);
        }

        public void ContinuousShot()
        {
            imageProvider.ContinuousShot();
        }

        public void Close()
        {
            imageProvider.Close();
        }

        public void Stop()
        {
            imageProvider.Stop();
        }

        public ImageProvider.Image GetLatestImage()
        {
            return imageProvider.GetLatestImage();
        }

        public void ReleaseImage()
        {
            imageProvider.ReleaseImage();
        }
    }
}
