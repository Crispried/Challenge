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
using Challange.Domain.Servuces.Video.Concrete;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Event;

namespace Challange.Presenter.Presenters.MainPresenter
{
    public partial class MainPresenter : BasePresenter<IMainView>
    {
        // settings
        private IFileService fileService;
        private IMessageParser messageParser;
        private PlayerPanelSettings playerPanelSetting;
        private ChallengeSettings challengeSetting;
        private FtpSettings ftpSetting;
        private RewindSettings rewindSetting;
        private GameInformation gameInformation;
        private ISettingsContext settingsContext;
        private INullSettingsContainer nullSettingsContainer;
        private IProcessStarter processStarter;
        private IZoomer zoomer;


        // video streaming
        private ICamerasContainer camerasContainer;
        private ICameraProvider cameraProvider;

        // this is temporary object which will keep fps objects
        // from all cameras which we create every second
        private IFpsContainer fpsContainer;
        private IChallengeBuffers challengeBuffers;
        private IInternalChallengeTimer internalChallengeTimer;
        private IChallengeObject challenge;
        private IEventSubscriber eventSubscriber;



        public MainPresenter(IApplicationController controller,
                             IMainView mainView,
                             IFileService fileService,
                             IMessageParser messageParser,
                             ISettingsContext settingsContext,
                             INullSettingsContainer nullSettingsContainer,
                             ICameraProvider cameraProvider,
                             ICamerasContainer camerasContainer,
                             IProcessStarter processStarter,
                             IZoomer zoomer,
                             IChallengeBuffers challengeBuffers,
                             IFpsContainer fpsContainer,
                             IInternalChallengeTimer internalChallengeTimer,
                             IChallengeObject challenge,
                             IEventSubscriber eventSubscriber) : 
                             base(controller, mainView)
        {
            this.fileService = fileService;
            this.messageParser = messageParser;
            this.settingsContext = settingsContext;
            this.nullSettingsContainer = nullSettingsContainer;
            this.cameraProvider = cameraProvider;
            this.camerasContainer = camerasContainer;
            this.processStarter = processStarter;
            this.zoomer = zoomer;
            this.challengeBuffers = challengeBuffers;
            this.fpsContainer = fpsContainer;
            this.internalChallengeTimer = internalChallengeTimer;
            this.challenge = challenge;
            this.eventSubscriber = eventSubscriber;
            SubscribePresenters();
            gameInformation = new GameInformation();
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
            View.PassCamerasNamesToPresenterCallback += PassCamerasNamesToPresenter;
            View.OpenChallengePlayerForLastChallenge += OpenChallengePlayerForLastChallenge;
            View.OpenBroadcastForm += OpenBroadcastForm;
        }
        
        public GameInformation GameInformation
        {
            set
            {
                gameInformation = value;
            }
        }   

        public InternalChallengeTimer InternalChallengeTimer
        {
            set
            {
                internalChallengeTimer = value;
            }
        }

        public ChallengeSettings ChallengeSettings
        {
            set
            {
                challengeSetting = value;
            }
        }

        public FtpSettings FtpSettings
        {
            set
            {
                ftpSetting = value;
            }
        }
    }
}
