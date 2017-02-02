using Challange.Domain.Services.Message;
using Challange.Domain.Services.PlayVideo.Abstract;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;
using Challange.Domain.Services.Zoom.Abstract;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Challange.Presenter.Presenters.ChallengePlayerPresenter
{
    public partial class ChallengePlayerPresenter : BasePresenter<IChallengePlayerView, Tuple<string, RewindSettings>>
    {
        private RewindSettings rewindSettings;

        private IVideoPlayer _videoPlayer;

        private IChallengeReader _challengeReader;

        private IMessageParser _messageParser;

        private IZoomer _zoomer;

        public ChallengePlayerPresenter(IApplicationController controller,
                                        IChallengePlayerView mainView,
                                        IMessageParser messageParser,
                                        IVideoPlayer videoPlayer,
                                        IChallengeReader challengeReader,
                                        IZoomer zoomer) : 
                                        base(controller, mainView)
        {
            _messageParser = messageParser;
            _videoPlayer = videoPlayer;
            _challengeReader = challengeReader;
            _zoomer = zoomer;
            SubscribePresenters();
        }

        private void SubscribePresenters()
        {
            View.OpenBroadcastForm += OpenBroadcastForm;
            View.StartAllPlayers += StartAllPlayers;
            View.StopAllPlayers += StopAllPlayers;
            View.RewindBackward += RewindBackward;
            View.RewindForward += RewindForward;
            View.OnFormClosing += OnFormClosing;
            View.OnPlaybackSpeedChanged += PlaybackSpeedChanged;
            View.MakeZoom += MakeZoom;
        }
    }
}
