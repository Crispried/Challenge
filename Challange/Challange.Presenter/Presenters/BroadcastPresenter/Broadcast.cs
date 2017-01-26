using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.PlayVideo.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;

namespace Challange.Presenter.Presenters.BroadcastPresenter
{
    public enum BroadcastType
    {
        Stream, Replay
    }

    public partial class BroadcastPresenter : BasePresenter<IBroadcastView, Tuple<object, BroadcastType>>
    {
        private object objectToBroadcast;
        private BroadcastType broadcastType;
        private ICamerasProvider _camerasProvider;
        private IVideoPlayer _videoPlayer;
     
        public BroadcastPresenter(
                IApplicationController controller,
                IBroadcastView broadcastView,
                ICamerasProvider camerasProvider,
                IVideoPlayer videoPlayer) :
                base(controller, broadcastView)
        {
            _camerasProvider = camerasProvider;
            _videoPlayer = videoPlayer;
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.BroadcastShowCallback += BroadcastShowCallback;
        }
    }
}
