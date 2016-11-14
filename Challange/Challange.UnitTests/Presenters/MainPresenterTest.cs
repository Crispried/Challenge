using Challange.Domain.Abstract;
using Challange.Domain.Entities;
using Challange.Domain.Services.Event;
using Challange.Domain.Services.FileSystem;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Replay;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Servuces.Video.Concrete;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.CamerasPresenter;
using Challange.Presenter.Presenters.ChallengeSettingsPresenter;
using Challange.Presenter.Presenters.FtpSettingsPresenter;
using Challange.Presenter.Presenters.GameInformationPresenter;
using Challange.Presenter.Presenters.MainPresenter;
using Challange.Presenter.Presenters.PlayerPanelSettingsPresenter;
using Challange.Presenter.Presenters.RewindSettingsPresenter;
using Challange.Presenter.Views;
using Challange.Presenter.Views.Layouts;
using NSubstitute;
using NUnit.Framework;
using System;

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
        private ICamerasContainer camerasContainer;
        private IProcessStarter processStarter;
        private IZoomer zoomer;
        private IChallengeBuffers challengeBuffers;
        private IFpsContainer fpsContainer;
        private IInternalChallengeTimer internalChallengeTimer;
        private IChallengeObject challengeObject;
        private IEventSubscriber eventSubscriber;
        private IMainFormLayout mainFormLayout;

        private PlayerPanelSettings playerPanelSettings;
        private ChallengeSettings challengeSettings;
        private FtpSettings ftpSettings;
        private RewindSettings rewindSettings;
        private GameInformation gameInformation;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IMainView>();
            fileService = Substitute.For<IFileService>();
            messageParser = Substitute.For<IMessageParser>();
            settingsContext = Substitute.For<ISettingsContext>();
            nullSettingsContainer = Substitute.For<INullSettingsContainer>();
            camerasContainer = Substitute.For<ICamerasContainer>();
            processStarter = Substitute.For<IProcessStarter>();
            zoomer = Substitute.For<IZoomer>();
            challengeBuffers = Substitute.For<IChallengeBuffers>();
            fpsContainer = Substitute.For<IFpsContainer>();
            internalChallengeTimer = Substitute.For<IInternalChallengeTimer>();
            challengeObject = Substitute.For<IChallengeObject>();
            eventSubscriber = Substitute.For<IEventSubscriber>();

            presenter = new MainPresenter(controller, view,
                                    fileService, messageParser,
                                    settingsContext, nullSettingsContainer,
                                    camerasContainer,
                                    processStarter, zoomer,
                                    challengeBuffers, fpsContainer,
                                    internalChallengeTimer, challengeObject,
                                    eventSubscriber);
            playerPanelSettings = InitializePlayerPanelSettings();
            challengeSettings = InitializeChallengeSettings();
            ftpSettings = InitializeFtpSettings();
            rewindSettings = InitializeRewindSettings();
            gameInformation = Substitute.For<GameInformation>();
        }

        [Test]
        public void RunParsingSettings()
        {
            // Arrange
            // Act
            presenter.Run();
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
            presenter.Run();
            // Assume
            nullSettingsContainer.ReceivedWithAnyArgs().CheckPlayerPanelSettingOnNull(null);
            nullSettingsContainer.ReceivedWithAnyArgs().CheckChallengeSettingOnNull(null);
            nullSettingsContainer.ReceivedWithAnyArgs().CheckFtpSettingOnNull(null);
            nullSettingsContainer.ReceivedWithAnyArgs().CheckRewindSettingOnNull(null);
        }

        [Test]
        public void RunIfThereAreNullSettings()
        {
            nullSettingsContainer.IsEmpty().ReturnsForAnyArgs(false);
            presenter.Run();
            // Arrange
            // Act
            presenter.Run();
            // Assume
            nullSettingsContainer.Received().IsEmpty();
            var returnedMessage = messageParser.Received().GetMessage(MessageType.SettingsFilesParseProblem);
            view.Received().ShowMessage(returnedMessage);
            controller.DidNotReceiveWithAnyArgs().Run<GameInformationPresenter,
                               GameInformation>(null);
            camerasContainer.DidNotReceiveWithAnyArgs().InitializeCameras();
            view.DidNotReceive().Show();
        }

        [Test]
        public void RunIfThereAreNotNullSettings()
        {
            nullSettingsContainer.IsEmpty().ReturnsForAnyArgs(true);
            // Arrange
            // Act
            presenter.Run();
            // Assume
            nullSettingsContainer.Received().IsEmpty();
            var returnedMessage = messageParser.DidNotReceiveWithAnyArgs().GetMessage(default(MessageType));
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
            controller.ReceivedWithAnyArgs().Run<GameInformationPresenter,
                               GameInformation>(gameInformation);
            camerasContainer.Received().InitializeCameras();
            view.Received().Show();
        }

        [Test]
        public void ShowDevicesList()
        {
            // Arrange
            // Act
            presenter.ShowDevicesList();
            // Assert
            controller.Received().Run<CamerasPresenter, ICamerasContainer>(camerasContainer); ;
        }

        [Test]
        public void AddNewFrame()
        {
            // Arrange
            view.CurrentFrameCameraName.Returns("");
            view.CurrentFrame.Returns(default(System.Drawing.Bitmap));
             IFps tempFps = Substitute.For<IFps>();
            fpsContainer.GetFpsByKey("").Returns(tempFps);
            // Act
            presenter.AddNewFrame();
            // Assert
            var cameraName = view.Received().CurrentFrameCameraName;
            var currentFrame = view.Received().CurrentFrame;       
            fpsContainer.Received().GetFpsByKey(cameraName);
            tempFps.ReceivedWithAnyArgs().AddFrame(currentFrame);
        }

        [Test]
        public void MakeZoom()
        {
            // Arrange
            var pictureBoxLocation = new System.Drawing.Point();
            var delta = 1;
            var mouseLocation = new System.Drawing.Point();
            // Act
            presenter.MakeZoom(pictureBoxLocation, delta, mouseLocation);
            // Assert
            ZoomData zoomData = zoomer.Received().MakeZoom(pictureBoxLocation, delta, mouseLocation);
            view.Received().RedrawZoomedImage(zoomData);
        }

        [Test]
        public void SaveCamerasNames()
        {
            // Arrange
            var key = "test";
            var cameraName = "1";
            // Act
            presenter.SaveCamerasNames(key, cameraName);
            // Assert
            camerasContainer.Received().SetCameraName(key, cameraName);
        }

        [Test]
        public void ChangePlayerPanelSettings()
        {
            // Arrange
            // Act
            presenter.ChangePlayerPanelSettings();
            // Assert
            controller.Received().Run<PlayerPanelSettingsPresenter,
                                PlayerPanelSettings>(settingsContext.PlayerPanelSetting);
            view.Received().DrawPlayers(settingsContext.PlayerPanelSetting, camerasContainer.CamerasNumber);
        }

        [Test]
        public void ChangeChallengeSettings()
        {
            // Arrange
            // Act
            presenter.ChangeChallengeSettings();
            // Assert
            controller.Received().Run<ChallengeSettingsPresenter,
                                ChallengeSettings>(settingsContext.ChallengeSetting);
        }

        [Test]
        public void ChangeFtpSettings()
        {
            // Arrange
            // Act
            presenter.ChangeFtpSettings();
            // Assert
            controller.Received().Run<FtpSettingsPresenter,
                                FtpSettings>(settingsContext.FtpSetting);
        }

        [Test]
        public void ChangeRewindSettings()
        {
            // Arrange
            // Act
            presenter.ChangeRewindSettings();
            // Assert
            controller.Received().Run<RewindSettingsPresenter,
                                RewindSettings>(settingsContext.RewindSetting);
        }

        [Test]
        public void StreamStartInitializeDevices()
        {
            // Arrange
            // Act
            presenter.StartStream();
            // Assert
            camerasContainer.Received().InitializeCameras();
        }

        [Test]
        public void StreamStartIfCamerasContainerIsEmpty()
        {
            // Arrange
            camerasContainer.IsEmpty().Returns(true);
            camerasContainer.GetCamerasNames.Returns(new System.Collections.Generic.List<string>() { "a", "b" });

            // Act
            presenter.StartStream();
            // Assert
            challengeBuffers.DidNotReceiveWithAnyArgs().SetNumberOfPastAndFutureElements(0, 0);
            var camerasNames = camerasContainer.DidNotReceive().GetCamerasNamesAsQueue;
            view.DidNotReceiveWithAnyArgs().BindPlayersToCameras(camerasNames);
            view.DidNotReceive().InitializeTimer();
            fpsContainer.DidNotReceiveWithAnyArgs().InitializeFpses(null);
            internalChallengeTimer.DidNotReceiveWithAnyArgs().EnableTimerEvent(default(Action));
            camerasContainer.ReceivedWithAnyArgs().StartAllCameras(null, null);

            //              ChangeStreamingStatus(true); NEED TO TEST

            var returnedMessage =
                messageParser.Received().GetMessage(MessageType.EmptyDeviceContainer);
            view.Received().ShowMessage(returnedMessage);
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
        public void CreateChallenge()
        {
            // Arrange
            // Act
         //   presenter.GameInformation = InitializeGameInformation();
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
    }
}
