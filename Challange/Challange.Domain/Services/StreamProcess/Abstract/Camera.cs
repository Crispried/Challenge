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

        public Bitmap GetCurrentFrame
        {
            get
            {
                return currentFrame;
            }
        }

        public delegate void NewFrameEventHandler(Bitmap frame);
        public event NewFrameEventHandler NewFrameEvent;

        protected void OnNewFrameEvent(Bitmap frame)
        {
            if (NewFrameEvent != null)
            {
                NewFrameEvent(frame);
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
