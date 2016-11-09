using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;
using Challange.Domain.Services.Event;
using System.Runtime.CompilerServices;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System.Diagnostics.CodeAnalysis;
using PylonC.NETSupportLibrary;
using Challange.Domain.Services.StreamProcess.Abstract;

namespace Challange.Domain.Services.StreamProcess.Concrete.Pylon
{
    [ExcludeFromCodeCoverage]
    public class Camera : ICamera
    {
        private Bitmap currentFrame;
        /// <summary>
        /// Full name is the name which you can get
        /// using your camera API, so it's the real name
        /// of camera, which you can get through API.
        /// Use this field as key value to get access
        /// to the camera
        /// </summary>
        private string fullName;
        /// <summary>
        /// Name is internal name of camera
        /// which can be customized by user
        /// </summary>
        private string name;

        private ImageProvider imageProvider;
        private uint cameraIndex;

        public Camera(uint cameraIndex, string fullName)
        {
            this.cameraIndex = cameraIndex;
            this.name = cameraIndex.ToString();
            this.fullName = FilterFullName(fullName);
            imageProvider = new ImageProvider();

            imageProvider.ImageReadyEvent += OnImageReadyEventCallback;
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

        public Bitmap GetCurrentFrame
        {
            get
            {
                return currentFrame;
            }
        }

        public string FullName
        {
            get
            {
                return fullName;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public event EventHandler<NewFrameEventArgs> NewFrameEvent;

        protected void OnNewFrameEvent(Bitmap frame, string cameraName)
        {
            if (NewFrameEvent != null)
            {
                NewFrameEvent(this, new NewFrameEventArgs(frame, cameraName));
            }
        }

        public void Start()
        {
            try
            {
                imageProvider.Open(cameraIndex);
                imageProvider.ContinuousShot(); /* Start the grabbing of images until grabbing is stopped. */
            }
            catch (Exception e)
            {
                // ShowException(e, imageProvider.GetLastErrorMessage());
            }
        }

        public void Stop()
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

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Camera camera = obj as Camera;
            if (camera == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (name == camera.name) && (fullName == camera.fullName);
        }

        public bool Equals(ICamera camera)
        {
            if (camera == null)
            {
                return false;
            }
            return (name == camera.Name) && (fullName == camera.FullName);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ fullName.GetHashCode();
        }
    }

    [ExcludeFromCodeCoverage]
    public class NewFrameEventArgs : EventArgs
    {
        public Bitmap Frame { get; set; }
        public string CameraName { get; set; }
        public NewFrameEventArgs(Bitmap frame, string cameraName)
        {
            Frame = frame;
            CameraName = cameraName;
        }
    }
}
