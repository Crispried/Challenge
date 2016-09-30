using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Abstract;

namespace Challange.Domain.Entities
{
    public abstract class Camera : ICamera
    {
        protected Bitmap currentFrame;
        /// <summary>
        /// Full name is the name which you can get
        /// using your camera API, so it's the real name
        /// of camera, which you can get through API.
        /// Use this field as key value to get access
        /// to the camera
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
            set
            {
                name = value;
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

        public bool Equals(Camera camera)
        {
            if (camera == null)
            {
                return false;
            }
            return (name == camera.name) && (fullName == camera.fullName);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ fullName.GetHashCode();
        }
    }
}
