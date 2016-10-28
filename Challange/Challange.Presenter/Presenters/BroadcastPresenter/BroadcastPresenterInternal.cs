using Challange.Domain.Entities;
using System;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter
    {
        /// <summary>
        /// Subscribes camera on NewFrameEvent and starts it
        /// </summary>
        private void StartStream()
        {
            EventSubscriber.AddEventHandler(camera, "NewFrameEvent", Camera_NewFrameEvent);
            camera.Start();
        }

        /// <summary>
        /// Process new frame from device cameraFullName
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="cameraName"></param>
        private void Camera_NewFrameEvent(object sender, EventArgs args)
        {
            NewFrameEventArgs newFrameEventArgs = args as NewFrameEventArgs;
            View.DrawNewFrame(newFrameEventArgs.Frame, newFrameEventArgs.CameraName);
        }
    }
}
