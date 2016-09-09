using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Entities
{
    public abstract class Camera
    {
        protected Bitmap currentFrame;
        /// <summary>
        /// Full name is the name which you can get
        /// using your camera API, so it's the real name
        /// of camera, which you can get through API
        /// </summary>
        protected string fullName; 
        /// <summary>
        /// Name is internal name of camera
        /// which can be customized by user
        /// </summary>
        protected string name;

        public Camera(string name, string fullName)
        {
            this.name = name;
            this.fullName = fullName;
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
        }

        public delegate void NewFrameEventHandler(Bitmap frame, string cameraName);
        public event NewFrameEventHandler NewFrameEvent;

        protected void OnNewFrameEvent(Bitmap frame, string cameraName)
        {
            if (NewFrameEvent != null)
            {
                NewFrameEvent(frame, cameraName);
            }
        }

        /// <summary>
        /// Starts stream process
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Stops stream process
        /// </summary>
        public abstract void Stop();
    }
}
