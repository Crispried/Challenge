using Challange.Presenter.Base;
using Challange.Presenter.Views;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using Challange.Domain.Entities;
using System.Timers;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using System.Drawing;
using static PylonC.NETSupportLibrary.DeviceEnumerator;
using Challange.Domain.Services.StreamProcess.Concrete;
using Challange.Domain.Services.StreamProcess.Abstract;
using System.Linq;
using Challange.Domain.Services.Replay;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.FileSystem;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Event;
using Challange.Presenter.Views.Layouts;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter : BasePresenter<IMainView>
    {
        // settings
        private IFileService _fileService;
        private IMessageParser _messageParser;
        private ISettingsContext _settingsContext;
        private INullSettingsContainer _nullSettingsContainer;

        private GameInformation _gameInformation;
        private IZoomer _zoomer;

        // video streaming
        private ICamerasProvider _camerasProvider;
        private ICamerasContainer _camerasContainer;

        // this is temporary object which will keep fps objects
        // from all cameras which we create every second
        private IFpsContainer _fpsContainer;
        private IChallengeBuffers _challengeBuffers;
        private IInternalChallengeTimer _internalChallengeTimer;
        private IChallengeObject _challenge;
        private IEventSubscriber _eventSubscriber;

        public MainPresenter(IApplicationController controller,
                             IMainView mainView,
                             IFileService fileService,
                             IMessageParser messageParser,
                             ISettingsContext settingsContext,
                             INullSettingsContainer nullSettingsContainer,
                             ICamerasContainer camerasContainer,
                             ICamerasProvider camerasProvider,
                             IZoomer zoomer,
                             IChallengeBuffers challengeBuffers,
                             IFpsContainer fpsContainer,
                             IInternalChallengeTimer internalChallengeTimer,
                             IChallengeObject challenge,
                             IEventSubscriber eventSubscriber) : 
                             base(controller, mainView)
        {
            _fileService = fileService;
            _messageParser = messageParser;
            _settingsContext = settingsContext;
            _nullSettingsContainer = nullSettingsContainer;
            _camerasContainer = camerasContainer;
            _camerasProvider = camerasProvider;
            _zoomer = zoomer;
            _challengeBuffers = challengeBuffers;
            _fpsContainer = fpsContainer;
            _internalChallengeTimer = internalChallengeTimer;
            _challenge = challenge;
            _eventSubscriber = eventSubscriber;
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
            View.MakeZoom += MakeZoom;
            View.PassCamerasNamesToPresenterCallback += SaveCamerasNames;
            View.OpenChallengePlayerForLastChallenge += OpenChallengePlayerForLastChallenge;
            View.OpenBroadcastForm += OpenBroadcastForm;
        }
    }
}
