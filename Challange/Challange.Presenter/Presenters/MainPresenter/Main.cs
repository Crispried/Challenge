using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.Zoom.Abstract;
using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.Challenge;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter : BasePresenter<IMainView>
    {
        // settings
        private IPathFormatter _pathFormatter;
        private IFileService _fileService;
        private IMessageParser _messageParser;
        private ISettingsContext _settingsContext;
        private INullSettingsContainer _nullSettingsContainer;

        private GameInformation _gameInformation;

        // video streaming
        private ICamerasProvider _camerasProvider;
        private ICamerasContainer _camerasContainer;
        private IVideoContainer _videoContainer;

        // this is temporary object which will keep fps objects
        // from all cameras which we create every second
        private IFpsContainer _fpsContainer;
        private IChallengeBuffers _challengeBuffers;
        private IInternalChallengeTimer _internalChallengeTimer;
        private IChallengeObject _challenge;
        private IChallengeWriter _challengeWriter;

        public MainPresenter(IApplicationController controller,
                             IMainView mainView,
                             IFileService fileService,
                             IPathFormatter pathFormatter,
                             IMessageParser messageParser,
                             ISettingsContext settingsContext,
                             INullSettingsContainer nullSettingsContainer,
                             ICamerasContainer camerasContainer,
                             IVideoContainer videoContainer,
                             ICamerasProvider camerasProvider,
                             IChallengeWriter challengeWriter,
                             IChallengeBuffers challengeBuffers,
                             IFpsContainer fpsContainer,
                             IInternalChallengeTimer internalChallengeTimer,
                             IChallengeObject challenge) : 
                             base(controller, mainView)
        {
            _fileService = fileService;
            _pathFormatter = pathFormatter;
            _messageParser = messageParser;
            _settingsContext = settingsContext;
            _nullSettingsContainer = nullSettingsContainer;
            _camerasContainer = camerasContainer;
            _videoContainer = videoContainer;
            _camerasProvider = camerasProvider;
            _challengeWriter = challengeWriter;
            _challengeBuffers = challengeBuffers;
            _fpsContainer = fpsContainer;
            _internalChallengeTimer = internalChallengeTimer;
            _challenge = challenge;
            SubscribePresenters();
            _gameInformation = new GameInformation();
        }

        private void SubscribePresenters()
        {
            View.OpenPlayerPanelSettings +=
                        ChangePlayerPanelSettings;
            View.OpenChallengeSettings +=
                                    ChangeChallengeSettings;
            View.OpenFtpSettings +=
                        ChangeFtpSettings;
            View.OpenRewindSettings += ChangeRewindSettings;
            View.OpenDevicesList += ShowDevicesList;
            View.StartStream += StartStream;
            View.StopStream += StopStream;
            View.OpenGameFolder += OpenGameFolder;
            View.MainFormClosing += FormClosing;
            View.CreateChallange += CreateChallange;
            View.NewFrameCallback += AddNewFrame;
            View.OpenChallengePlayer += OpenChallengePlayer;
            View.OpenChallengePlayerForLastChallenge += OpenChallengePlayerForLastChallenge;
            View.OpenBroadcastForm += OpenBroadcastForm;
        }
    }
}
