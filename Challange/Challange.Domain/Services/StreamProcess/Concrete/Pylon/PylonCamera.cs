using Challange.Domain.Entities;
using Challange.Domain.Services.StreamProcess.Abstract;
using PylonC.NETSupportLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    public class PylonCamera : Camera
    {
        private ImageProvider imageProvider;
        private uint cameraIndex;

        public PylonCamera(uint cameraIndex, string fullName)
             : base(cameraIndex.ToString(), FilterFullName(fullName))
        {
            this.cameraIndex = cameraIndex;
            imageProvider = new ImageProvider();
            imageProvider.ImageReadyEvent +=
                new ImageProvider.ImageReadyEventHandler(
                            OnImageReadyEventCallback);
        }

        private void OnImageReadyEventCallback()
        {
            try
            {
                /* Acquire the image from the image provider. Only show the latest image. The camera may acquire images faster than images can be displayed*/
                ImageProvider.Image image = imageProvider.GetLatestImage();

                /* Check if the image has been removed in the meantime. */
                if (image != null)
                {
                    /* Check if the image is compatible with the currently used bitmap. */
                    if (BitmapFactory.IsCompatible(currentFrame, image.Width, image.Height, image.Color))
                    {
                        /* Update the bitmap with the image data. */
                        BitmapFactory.UpdateBitmap(currentFrame, image.Buffer, image.Width, image.Height, image.Color);
                    }
                    else /* A new bitmap is required. */
                    {
                        BitmapFactory.CreateBitmap(out currentFrame, image.Width, image.Height, image.Color);
                        BitmapFactory.UpdateBitmap(currentFrame, image.Buffer, image.Width, image.Height, image.Color);
                    }
                    /* The processing of the image is done. Release the image buffer. */
                    imageProvider.ReleaseImage();
                    /* The buffer can be used for the next image grabs. */
                }
                OnNewFrameEvent(currentFrame, FilterFullName(fullName));
            }
            catch (Exception e)
            {
                //ShowException(e, m_imageProvider.GetLastErrorMessage());
            }
        }

        public override void Start()
        {
            try
            {
                imageProvider.Open(cameraIndex);
                imageProvider.ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
            }
            catch (Exception e)
            {
                //ShowException(e, imageProvider.GetLastErrorMessage());
            }
        }

        public override void Stop()
        {
            /* Stop the grabbing. */
            try
            {               
                imageProvider.Close();
                imageProvider.Stop();
            }
            catch (Exception e)
            {
                //ShowException(e, imageProvider.GetLastErrorMessage());
            }
        }

        public static string FilterFullName(string fullName)
        {
            return fullName.Replace(":", "_port_");
        }
    }
}
