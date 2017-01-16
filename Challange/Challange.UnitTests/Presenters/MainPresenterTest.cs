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
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.BroadcastPresenter;
using Challange.Presenter.Presenters.CamerasPresenter;
using Challange.Presenter.Presenters.ChallengePlayerPresenter;
using Challange.Presenter.Presenters.ChallengeSettingsPresenter;
using Challange.Presenter.Presenters.FtpSettingsPresenter;
using Challange.Presenter.Presenters.GameInformationPresenter;
using Challange.Presenter.Presenters.MainPresenter;
using Challange.Presenter.Presenters.PlayerPanelSettingsPresenter;
using Challange.Presenter.Presenters.RewindSettingsPresenter;
using Challange.Presenter.Views;
using System.Collections.Generic;
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
        private ICamerasProvider _camerasProvider;
        private IZoomer zoomer;
        private IChallengeBuffers challengeBuffers;
        private IFpsContainer fpsContainer;
        private IInternalChallengeTimer internalChallengeTimer;
        private IChallengeObject challengeObject;
        private IEventSubscriber eventSubscriber;

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
            _camerasProvider = Substitute.For<ICamerasProvider>();
            zoomer = Substitute.For<IZoomer>();
            challengeBuffers = Substitute.For<IChallengeBuffers>();
            fpsContainer = Substitute.For<IFpsContainer>();
            internalChallengeTimer = Substitute.For<IInternalChallengeTimer>();
            challengeObject = Substitute.For<IChallengeObject>();
            eventSubscriber = Substitute.For<IEventSubscriber>();

            presenter = new MainPresenter(controller, view,
                                    fileService, messageParser,
                                    settingsContext, nullSettingsContainer,
                                    camerasContainer, _camerasProvider,
                                    zoomer,
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
            _camerasProvider.DidNotReceiveWithAnyArgs().InitializeCameras();
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
            _camerasProvider.Received().InitializeCameras();
            var camerasNames = camerasContainer.Received().GetCamerasNames();
            view.Received().DrawPlayers(settingsContext.PlayerPanelSetting, camerasContainer.CamerasNumber, camerasNames);
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
            var camerasNames = camerasContainer.Received().GetCamerasNames();
            view.Received().DrawPlayers(settingsContext.PlayerPanelSetting, camerasContainer.CamerasNumber, camerasNames);
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
        public void StreamStartIfCamerasContainerIsEmpty()
        {
            // Arrange
            camerasContainer.IsEmpty().Returns(true);
            camerasContainer.GetCamerasNames().Returns(new List<string>() { "a", "b" });
            // Act
            presenter.StartStream();
            // Assert
            _camerasProvider.Received().InitializeCameras();
            challengeBuffers.DidNotReceiveWithAnyArgs().SetNumberOfPastAndFutureElements(0, 0);
            var camerasNames = camerasContainer.DidNotReceive().GetCamerasNames();
            view.DidNotReceive().InitializeTimer();
            fpsContainer.DidNotReceiveWithAnyArgs().InitializeFpses(null);
            internalChallengeTimer.DidNotReceiveWithAnyArgs().EnableTimerEvent(default(Action));
            _camerasProvider.DidNotReceiveWithAnyArgs().StartAllCameras(null, null);
            view.DidNotReceiveWithAnyArgs().ToggleChallengeButton(true);
            view.DidNotReceiveWithAnyArgs().ToggleStartButton(false);
            view.DidNotReceiveWithAnyArgs().ToggleStopButton(true); view.DidNotReceive().ToggleVisibilityOfViewLastChallengeButton();
            var returnedMessage =
                messageParser.Received().GetMessage(MessageType.EmptyDeviceContainer);
            view.Received().ShowMessage(returnedMessage);
        }

        [Test]
        public void StreamStartIfCamerasContainerIsNotEmpty()
        {
            // Arrange
            camerasContainer.IsEmpty().Returns(false);
            camerasContainer.GetCamerasNames().Returns(new List<string>() { "a", "b" });
            settingsContext.ChallengeSetting.Returns(challengeSettings);
            var past = settingsContext.ChallengeSetting.NumberOfPastFPS;
            var future = settingsContext.ChallengeSetting.NumberOfFutureFPS;
            // Act
            presenter.StartStream();
            // Assert
            _camerasProvider.Received().InitializeCameras();
            challengeBuffers.Received().SetNumberOfPastAndFutureElements(past, future);
            var camerasNames = camerasContainer.Received().GetCamerasNames();
            view.Received().InitializeTimer();
            fpsContainer.Received().InitializeFpses(camerasContainer.GetCamerasNames());
            internalChallengeTimer.ReceivedWithAnyArgs().EnableTimerEvent(default(Action));
            _camerasProvider.ReceivedWithAnyArgs().StartAllCameras(null, eventSubscriber);
            view.Received().ToggleChallengeButton(true);
            view.Received().ToggleStartButton(false);
            view.Received().ToggleStopButton(true);
            view.Received().ToggleVisibilityOfViewLastChallengeButton();
            var returnedMessage =
                messageParser.DidNotReceiveWithAnyArgs().GetMessage(default(MessageType));
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }

        [Test]
        public void StreamStop()
        {
            // Arrange
            // Act
            presenter.StopStream();
            // Assert
            _camerasProvider.Received().StopAllCameras();
            view.Received().ResetTimeAxis();
            view.Received().ToggleChallengeButton(false);
            view.Received().ToggleStartButton(true);
            view.Received().ToggleStopButton(false);
            view.Received().ToggleVisibilityOfViewLastChallengeButton();
        }

        [Test]
        public void CreateChallenge()
        {
            // Arrange
            settingsContext.ChallengeSetting.Returns(challengeSettings);
            var future = settingsContext.ChallengeSetting.NumberOfFutureFPS;
            // Act
            presenter.CreateChallange();
            // Assert
            var elapsedTime = view.Received().GetElapsedTime;
            challengeObject.Received().Initialize(gameInformation.DirectoryName, elapsedTime);
            fileService.Received().CreateDirectory(challengeObject.PathToChallengeDirectory);
            internalChallengeTimer.Received().DisableTimerEvent();
            internalChallengeTimer.ReceivedWithAnyArgs().EnableTimerEvent(null);
            view.MakeChallengeButtonInvisibleOn(future);
            view.MakeChallengeRecordingImageVisibleOn(future);
            view.AddMarkerOnTimeAxis(challengeObject.PathToChallengeDirectory);
        }

        [Test]
        public void OpenGameFolder()
        {
            // Arrange
            // Act
            presenter.OpenGameFolder();
            // Assert
            fileService.Received().OpenFileOrFolder(gameInformation.DirectoryName);
        }

        [Test]
        public void OpenChallengePlayer()
        {
            // Arrange
            var path = @"Team1_vs_Team2(21.10.2016)\00_00_10\";
            // Act
            presenter.OpenChallengePlayer(path);
            // Assert
            controller.Received().
                Run<ChallengePlayerPresenter, Tuple<string, RewindSettings>>(
                Tuple.Create(path, settingsContext.RewindSetting));
        }

        [Test]
        public void OpenChallengePlayerForLastChallengeIfChallengeIsNotNull()
        {
            // Arrange
            challengeObject.PathToChallengeDirectory = @"Team1_vs_Team2(21.10.2016)\00_00_10\";
            // Act
            presenter.OpenChallengePlayerForLastChallenge();
            // Assert
            controller.Received().
             Run<ChallengePlayerPresenter, Tuple<string, RewindSettings>>(
             Tuple.Create(challengeObject.PathToChallengeDirectory, settingsContext.RewindSetting));
            var returnedMessage = messageParser.DidNotReceiveWithAnyArgs().GetMessage(default(MessageType));
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }

        [Test]
        public void OpenChallengePlayerForLastChallengeIfChallengeIsNull()
        {
            // Arrange
            // Act
            presenter.OpenChallengePlayerForLastChallenge();
            // Assert
            controller.DidNotReceiveWithAnyArgs().
             Run<ChallengePlayerPresenter, Tuple<string, RewindSettings>>(null);
            var returnedMessage = messageParser.Received().GetMessage(MessageType.HaveNotRecordedAnyChallengeYet);
            view.Received().ShowMessage(returnedMessage);
        }

        [Test]
        public void PassCamerasNamesToPresenter()
        {
            // Arrange
            var key = "key";
            var cameraName = "cameraName";
            // Act
            presenter.PassCamerasNamesToPresenter(key, cameraName);
            // Assert
            camerasContainer.Received().SetCameraName(key, cameraName);
        }

        [Test]
        public void OpenBroadcastForm()
        {
            // Arrange
            var cameraFullName = "test";
            // Act
            presenter.OpenBroadcastForm(cameraFullName);
            // Assert
            var cameraForBroadcasting = camerasContainer.Received().GetCameraByKey(cameraFullName);
            controller.ReceivedWithAnyArgs().Run<BroadcastPresenter, ICamera>(cameraForBroadcasting);
        }

        [Test]
        public void FormClosing()
        {
            // Arrange
            // Act
            presenter.FormClosing();
            // Assert
            _camerasProvider.Received().StopAllCameras();
        }
    }
}
