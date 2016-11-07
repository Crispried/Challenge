using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Replay;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.ChallengeSettingsPresenter;
using Challange.Presenter.Presenters.GameInformationPresenter;
using Challange.Presenter.Presenters.MainPresenter;
using Challange.Presenter.Presenters.PlayerPanelSettingsPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class MainPresenterTest : TestCase
    {
        private const string pathToChallengeSettings = @"settings_test\challenge.xml";
        private const string pathToPlayerPanelSettings = @"settings_test\player_panel.xml";
        private IApplicationController controller;
        private MainPresenter presenter;
        private IMainView view;
        private IMessageParser messageParser;
        private IFileService fileService;
        private ISettingsContext settingsContext;
        private INullSettingsContainer nullSettingsContainer;
        private ICameraProvider cameraProvider;
        private ICamerasContainer camerasContainer;
        private IProcessStarter processStarter;
        private IZoomer zoomer;

        private PlayerPanelSettings playerPanelSettings;
        private ChallengeSettings challengeSettings;
        private FtpSettings ftpSettings;
        private RewindSettings rewindSettings;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IMainView>();
            fileService = Substitute.For<IFileService>();
            messageParser = Substitute.For<IMessageParser>();
            settingsContext = Substitute.For<ISettingsContext>();
            cameraProvider = Substitute.For<ICameraProvider>();
            nullSettingsContainer = Substitute.For<INullSettingsContainer>();
            camerasContainer = Substitute.For<ICamerasContainer>();
            processStarter = Substitute.For<IProcessStarter>();
            zoomer = Substitute.For<IZoomer>();
            presenter = new MainPresenter(controller, view,
                                    fileService, messageParser,
                                    settingsContext, nullSettingsContainer,
                                    cameraProvider, camerasContainer,
                                    processStarter, zoomer);
            presenter.Run();
            playerPanelSettings = InitializePlayerPanelSettings();
            challengeSettings = InitializeChallengeSettings();
            ftpSettings = InitializeFtpSettings();
            rewindSettings = InitializeRewindSettings();
        }

        [Test]
        public void RunParsingSettings()
        {
            // Arrange
            // Act
            // Assume
            settingsContext.Received().GetPlayerPanelSetting();
            settingsContext.Received().GetChallengeSetting();
            settingsContext.Received().GetFtpSetting();
            settingsContext.Received().GetRewindSetting();
        }

        [Test]
        public void RunCheckingSettingsOnNull()
        {
            // Arrange
            // Act
            // Assume
            nullSettingsContainer.ReceivedWithAnyArgs().CheckPlayerPanelSettingOnNull(null);
            nullSettingsContainer.ReceivedWithAnyArgs().CheckChallengeSettingOnNull(null);
            nullSettingsContainer.ReceivedWithAnyArgs().CheckFtpSettingOnNull(null);
            nullSettingsContainer.ReceivedWithAnyArgs().CheckRewindSettingOnNull(null);
        }

        [Test]
        public void RunIfThereAreNullSettings()
        {
            nullSettingsContainer.Received().CheckRewindSettingOnNull(null);
            // Arrange
            // Act
            // Assume
            nullSettingsContainer.Received().IsEmpty();
            var returnedMessage = messageParser.Received().GetMessage(MessageType.SettingsFilesParseProblem);
            view.ShowMessage(returnedMessage);
            controller.DidNotReceiveWithAnyArgs().Run<GameInformationPresenter,
                               GameInformation>(null);
            cameraProvider.DidNotReceiveWithAnyArgs().GetConnectedCameras();
            camerasContainer.DidNotReceiveWithAnyArgs().InitializeCameras(null);
        }

        [Test]
        public void RunIfPlayerPanelSettingsAreNull()
        {
            // Arrange
            //playerPanelSettingsService.Received().GetSetting().Returns(playerPanelSettings);
            //challengeSettingsService.Received().GetSetting().Returns(default(ChallengeSettings));
            //ftpSettingsService.Received().GetSetting().Returns(default(FtpSettings));
            // Act
            // Assume
            controller.DidNotReceiveWithAnyArgs().Run<GameInformationPresenter, GameInformation>(default(GameInformation));
        }

        [Test]
        public void RunIfFtpSettingsAreNull()
        {

        }

        [Test]
        public void RunIfEverythingOk()
        {

        }

        [Test]
        public void StreamStart()
        {
            // Arrange
            // Act
            view.StartStream += Raise.Event<Action>();
            // Assert
            if (!presenter.IsDeviceListEmpty)
            {
                Assert.IsTrue(presenter.AreCamerasBindedToPlayers);
                Assert.IsTrue(presenter.WasTimeAxisTimerInitialized);
                Assert.IsTrue(presenter.WasRecordingFpsTimerInitialized);
                Assert.IsTrue(presenter.IsCaptureDevicesEnable);
                Assert.IsTrue(presenter.IsStreamProcessOn);
            }
            else
            {
                Assert.IsTrue(presenter.WasDeviceListEmptyMessageShowed);
            }
        }

        [Test]
        public void StreamStop()
        {
            // Arrange
            // Act
            view.StopStream += Raise.Event<Action>();
            // Assert
            Assert.IsFalse(presenter.IsCaptureDevicesEnable);
            Assert.IsTrue(presenter.WasTimeAxisResetted);
            Assert.IsFalse(presenter.IsStreamProcessOn);
        }

        [Test]
        public void ShowDevicesList()
        {
            // Arrange
            // Act
            view.OpenDevicesList += Raise.Event<Action>();
            // Assert
        }

        [Test]
        public void AddNewFrame()
        {
            // Arrange
            // Act
            view.NewFrameCallback += Raise.Event<Action>();
            // Assert
        }

        [Test]
        public void ChangePlayerPanelSettings()
        {
            // Arrange
            // Act
            view.OpenPlayerPanelSettings += Raise.Event<Action>();
            // Assert
            controller.Received().Run<PlayerPanelSettingsPresenter,
                                PlayerPanelSettings>(playerPanelSettings);
        }

        [Test]
        public void ChangeChallengeSettings()
        {
            // Arrange
            // Act
            view.OpenChallengeSettings += Raise.Event<Action>();
            // Assert
            controller.Received().Run<ChallengeSettingsPresenter,
                                ChallengeSettings>(challengeSettings);
        }

        [Test]
        public void CreateChallenge()
        {
            // Arrange
            // Act
            presenter.GameInformation = InitializeGameInformation();
            presenter.InternalChallengeTimer = InitializeInternalChallengeTimer();
            //presenter.ChallengeSettings = InitializeChallengeSettings();
            view.CreateChallange += Raise.Event<Action>();
            // Assert
            Assert.IsTrue(presenter.ElapsedTimeWasGot);
            Assert.IsTrue(presenter.DirectoryForChallengeWasCreated);
            Assert.IsFalse(presenter.IsEventForPastFramesActive);
            Assert.IsTrue(presenter.IsEventForFutureFramesActive);
            Assert.IsFalse(presenter.IsChallengeButtonVisible);
            Assert.IsTrue(presenter.MarkerWasAddedOntoTimeAxis);
        }

        [Test]
        public void OpenGameFolder()
        {
            // Arrange
            // Act
            view.OpenGameFolder += Raise.Event<Action>();
            // Assert
        }

        [Test]
        public void FormClosing()
        {
            // Arrange
            // Act
            view.MainFormClosing += Raise.Event<Action>();
            // Assert
        }

        private GameInformation InitializeGameInformation()
        {
            GameInformation gameInformation = new GameInformation()
            {
                FirstTeam = "Red",
                SecondTeam = "Blue",
                Date = "09.08.2016",
                GameStart = "18:36:00",
                Country = "England",
                City = "London",
                Part = "2"
            };
            return gameInformation;
        }

        private InternalChallengeTimer InitializeInternalChallengeTimer()
        {
            return new InternalChallengeTimer(1000, true);
        }
    }
}
