using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter
    {
        /// <summary>
        /// Subscribes camera on NewFrameEvent and starts it
        /// </summary>
        private void StartStream()
        {
            eventSubscriber.AddEventHandler(camera, "NewFrameEvent", Camera_NewFrameEvent);
            camera.Start();
        }

        [ExcludeFromCodeCoverage]
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
