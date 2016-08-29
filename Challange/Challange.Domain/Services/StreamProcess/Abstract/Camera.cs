using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.StreamProcess.Abstract
{
    public abstract class Camera
    {
        protected Bitmap currentFrame;
        protected string name;

        public Camera(string name)
        {
            this.name = name;
        }

        public Bitmap GetCurrentFrame
        {
            get
            {
                return currentFrame;
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
