using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using Challange.Domain.Services.Video.Concrete;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public partial class BroadcastPresenter
    {
        /// <summary>
        /// Subscribes camera on NewFrameEvent and starts it
        /// </summary>
        private void StartBroadcasting()
        {
            if(broadcastType == BroadcastType.Stream)
            {
                var camera = (ICamera)objectToBroadcast;
                _camerasProvider.StartCamera(camera, Camera_NewFrameEvent);
            }
            else if(broadcastType == BroadcastType.Replay)
            {
                var video = (Video)objectToBroadcast;
                _videoPlayer.DrawAction = DrawAction;
                _videoPlayer.PlayVideo(video);
            }
        }

        [ExcludeFromCodeCoverage]
        private void DrawAction(Bitmap frame, string videoName)
        {
            View.DrawNewFrame(frame, videoName);
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
