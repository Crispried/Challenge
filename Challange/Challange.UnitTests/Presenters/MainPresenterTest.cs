using Challange.Domain.Services.Message;
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
using Challange.Domain.Services.FileSystem.Abstract;
using Challange.Domain.Services.Challenge;

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
        private IPathFormatter _pathFormatter;
        private ISettingsContext _settingsContext;
        private INullSettingsContainer nullSettingsContainer;
        private IVideoContainer _videoContainer;
        private ICamerasProvider _camerasProvider;
        private IChallengeWriter _challengeWriter;
        private IChallengeBuffers _challengeBuffers;
        private IFpsContainer _fpsContainer;
        private IInternalChallengeTimer _internalChallengeTimer;
        private IChallengeObject _challengeObject;

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
            _pathFormatter = Substitute.For<IPathFormatter>();
            messageParser = Substitute.For<IMessageParser>();
            _settingsContext = Substitute.For<ISettingsContext>();
            nullSettingsContainer = Substitute.For<INullSettingsContainer>();
            _videoContainer = Substitute.For<IVideoContainer>();
            _camerasProvider = Substitute.For<ICamerasProvider>();
            _challengeWriter = Substitute.For<IChallengeWriter>();
            _challengeBuffers = Substitute.For<IChallengeBuffers>();
            _fpsContainer = Substitute.For<IFpsContainer>();
            _internalChallengeTimer = Substitute.For<IInternalChallengeTimer>();
            _challengeObject = Substitute.For<IChallengeObject>();

            presenter = new MainPresenter(controller, view,
                                    fileService, _pathFormatter, messageParser,
                                    _settingsContext, nullSettingsContainer,
                                    _videoContainer,
                                    _camerasProvider, _challengeWriter,
                                    _challengeBuffers, _fpsContainer,
                                    _internalChallengeTimer, _challengeObject);
            playerPanelSettings = InitializePlayerPanelSettings();
            challengeSettings = InitializeChallengeSettings();
            ftpSettings = InitializeFtpSettings();
            rewindSettings = InitializeRewindSettings();
            _camerasProvider.CamerasContainer.Returns(Substitute.For<ICamerasContainer>());
            gameInformation = Substitute.For<GameInformation>();
        }

        [Test]
        public void RunParsingSettings()
        {
            // Arrange
            // Act
            presenter.Run();
            // Assume
            _settingsContext.Received().GetPlayerPanelSetting();
            _settingsContext.Received().GetChallengeSetting();
            _settingsContext.Received().GetFtpSetting();
            _settingsContext.Received().GetRewindSetting();
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
            var camerasKeys = _camerasProvider.CamerasContainer.Received().GetCamerasKeys();
            view.Received().DrawPlayers(_settingsContext.PlayerPanelSetting,
                                        _camerasProvider.CamerasContainer.CamerasNumber, camerasKeys);
            view.Received().Show();
        }

        [Test]
        public void ShowDevicesList()
        {
            // Arrange
            // Act
            presenter.ShowDevicesList();
            // Assert
            controller.Received().Run<CamerasPresenter, ICamerasContainer>(_camerasProvider.CamerasContainer); ;
        }

        [Test]
        public void AddNewFrame()
        {
            // Arrange
            view.CurrentFrameCameraName.Returns("");
            view.CurrentFrame.Returns(default(System.Drawing.Bitmap));
             IFps tempFps = Substitute.For<IFps>();
            _fpsContainer.GetFpsByKey("").Returns(tempFps);
            // Act
            presenter.AddNewFrame();
            // Assert
            var cameraName = view.Received().CurrentFrameCameraName;
            var currentFrame = view.Received().CurrentFrame;       
            _fpsContainer.Received().GetFpsByKey(cameraName);
            tempFps.ReceivedWithAnyArgs().AddFrame(currentFrame);
        }

        [Test]
        public void ChangePlayerPanelSettingsIfShoudNotRedrawPlayersTest()
        {
            // Arrange
            _settingsContext.PlayerPanelSetting.Returns(InitializePlayerPanelSettings());
            // Act
            presenter.ChangePlayerPanelSettings();
            // Assert
            controller.Received().Run<PlayerPanelSettingsPresenter,
                                PlayerPanelSettings>(_settingsContext.PlayerPanelSetting);
        }

        [Test]
        public void ChangePlayerPanelSettingsIfShoudRedrawPlayersTest()
        {
            // Arrange
            var settings = Substitute.For<PlayerPanelSettings>();
            settings.AutosizeMode = true;
            settings.PlayerHeight = 480;
            settings.PlayerWidth = 600;
            _settingsContext.PlayerPanelSetting.Returns(settings);
            settings.Equals(InitializePlayerPanelSettings()).ReturnsForAnyArgs(false);
            // Act
            presenter.ChangePlayerPanelSettings();
            // Assert
            controller.Received().Run<PlayerPanelSettingsPresenter,
                                PlayerPanelSettings>(_settingsContext.PlayerPanelSetting);
            var camerasKeys = _camerasProvider.CamerasContainer.Received().GetCamerasKeys();
            view.Received().DrawPlayers(_settingsContext.PlayerPanelSetting,
                                        _camerasProvider.CamerasContainer.CamerasNumber, camerasKeys);
        }

        [Test]
        public void ChangeChallengeSettings()
        {
            // Arrange
            // Act
            presenter.ChangeChallengeSettings();
            // Assert
            controller.Received().Run<ChallengeSettingsPresenter,
                                ChallengeSettings>(_settingsContext.ChallengeSetting);
        }

        [Test]
        public void ChangeFtpSettings()
        {
            // Arrange
            // Act
            presenter.ChangeFtpSettings();
            // Assert
            controller.Received().Run<FtpSettingsPresenter,
                                FtpSettings>(_settingsContext.FtpSetting);
        }

        [Test]
        public void ChangeRewindSettings()
        {
            // Arrange
            // Act
            presenter.ChangeRewindSettings();
            // Assert
            controller.Received().Run<RewindSettingsPresenter,
                                RewindSettings>(_settingsContext.RewindSetting);
        }

        [Test]
        public void StreamStartIfCamerasContainerIsEmpty()
        {
            // Arrange
            _camerasProvider.CamerasContainer.IsEmpty().Returns(true);
            _camerasProvider.CamerasContainer.GetCamerasNames().Returns(new List<string>() { "a", "b" });
            // Act
            presenter.StartStream();
            // Assert
            var camerasNames = _camerasProvider.CamerasContainer.DidNotReceive().GetCamerasNames();
            view.DidNotReceive().InitializeTimer();
            _fpsContainer.DidNotReceiveWithAnyArgs().InitializeFpses(null);
            _internalChallengeTimer.DidNotReceiveWithAnyArgs().EnableTimerEvent(default(Action));
            _camerasProvider.DidNotReceiveWithAnyArgs().StartAllCameras(null);
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
            _camerasProvider.CamerasContainer.IsEmpty().Returns(false);
            _camerasProvider.CamerasContainer.GetCamerasNames().Returns(new List<string>() { "a", "b" });
            _settingsContext.ChallengeSetting.Returns(challengeSettings);
            var past = _settingsContext.ChallengeSetting.NumberOfPastFPS;
            var future = _settingsContext.ChallengeSetting.NumberOfFutureFPS;
            // Act
            presenter.StartStream();
            // Assert
            var camerasNames = _camerasProvider.CamerasContainer.Received().GetCamerasNames();
            view.Received().InitializeTimer();
            _fpsContainer.Received().InitializeFpses(_camerasProvider.CamerasContainer.GetCamerasNames());
            _internalChallengeTimer.ReceivedWithAnyArgs().EnableTimerEvent(default(Action));
            _camerasProvider.ReceivedWithAnyArgs().StartAllCameras(null);
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
            _settingsContext.ChallengeSetting.Returns(challengeSettings);
            var future = _settingsContext.ChallengeSetting.NumberOfFutureFPS;
            // Act
            presenter.CreateChallange();
            // Assert
            var elapsedTime = view.Received().GetElapsedTime;
            _challengeObject.Received().Initialize(gameInformation.DirectoryName, elapsedTime);

            var pathToRootDirectory = _challengeObject.Received().PathToRootDirectory;
            var challengeFolderName = _challengeObject.Received().ChallengeFolderName;
            _challengeObject.PathToChallengeDirectory =
                _pathFormatter.Received().FormatPath(pathToRootDirectory, challengeFolderName);

            fileService.Received().CreateDirectory(_challengeObject.PathToChallengeDirectory);

            _internalChallengeTimer.Received().DisableTimerEvent();

            _internalChallengeTimer.Received().EnableTimerEvent(presenter.InternalTimerEventForFutureFrames);

            view.MakeChallengeButtonInvisibleOn(future);
            view.MakeChallengeRecordingImageVisibleOn(future);

            view.AddMarkerOnTimeAxis(_challengeObject.PathToChallengeDirectory);
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
                Tuple.Create(path, _settingsContext.RewindSetting));
        }

        [Test]
        public void OpenChallengePlayerForLastChallengeIfChallengeIsNotNull()
        {
            // Arrange
            _challengeObject.PathToChallengeDirectory = @"Team1_vs_Team2(21.10.2016)\00_00_10\";
            // Act
            presenter.OpenChallengePlayerForLastChallenge();
            // Assert
            controller.Received().
             Run<ChallengePlayerPresenter, Tuple<string, RewindSettings>>(
             Tuple.Create(_challengeObject.PathToChallengeDirectory, _settingsContext.RewindSetting));
            var returnedMessage = messageParser.DidNotReceiveWithAnyArgs().GetMessage(default(MessageType));
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }

        [Test]
        public void OpenChallengePlayerForLastChallengeIfChallengeIsNull()
        {
            // Arrange
            _challengeObject = null;
            presenter = new MainPresenter(controller, view,
                        fileService, _pathFormatter, messageParser,
                        _settingsContext, nullSettingsContainer,
                        _videoContainer,
                        _camerasProvider, _challengeWriter,
                        _challengeBuffers, _fpsContainer,
                        _internalChallengeTimer, _challengeObject);
            // Act
            presenter.OpenChallengePlayerForLastChallenge();
            // Assert
            controller.DidNotReceiveWithAnyArgs().
             Run<ChallengePlayerPresenter, Tuple<string, RewindSettings>>(null);
            var returnedMessage = messageParser.Received().GetMessage(MessageType.HaveNotRecordedAnyChallengeYet);
            view.Received().ShowMessage(returnedMessage);
        }

        [Test]
        public void CameraNameChangedTest()
        {
            // Arrange
            var key = "key";
            var cameraName = "cameraName";
            // Act
            presenter.CameraNameChanged(key, cameraName);
            // Assert
            _camerasProvider.CamerasContainer.Received().SetCameraName(key, cameraName);
        }

        [Test]
        public void OpenBroadcastForm()
        {
            // Arrange
            var cameraFullName = "test";
            // Act
            presenter.OpenBroadcastForm(cameraFullName);
            // Assert
            var cameraForBroadcasting = _camerasProvider.CamerasContainer.Received().GetCameraByKey(cameraFullName);
            controller.ReceivedWithAnyArgs().Run<BroadcastPresenter, Tuple<object, BroadcastType>>(Tuple.Create((object)cameraForBroadcasting, BroadcastType.Stream));
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

        [Test]
        public void InternalTimerEventForPastFramesIfHaveToRemovePastFpsTest()
        {
            // Arrange
            var challengeSettings = InitializeChallengeSettings();
            _settingsContext.ChallengeSetting.Returns(challengeSettings);
            _challengeBuffers.HaveToRemovePastFps(challengeSettings.NumberOfPastFPS).Returns(true);
            // Act
            presenter.InternalTimerEventForPastFrames();
            // Assert
            _challengeBuffers.Received().RemoveFirstFpsFromPastBuffer();
            _challengeBuffers.Received().AddPastFpses(_fpsContainer);
            _fpsContainer.Received().InitializeFpses(_camerasProvider.CamerasContainer.GetCamerasNames());
        }

        [Test]
        public void InternalTimerEventForPastFramesIfHaveNotToRemovePastFpsTest()
        {
            // Arrange
            var challengeSettings = InitializeChallengeSettings();
            _settingsContext.ChallengeSetting.Returns(challengeSettings);
            _challengeBuffers.HaveToRemovePastFps(challengeSettings.NumberOfPastFPS).Returns(false);
            // Act
            presenter.InternalTimerEventForPastFrames();
            // Assert
            _challengeBuffers.Received().AddPastFpses(_fpsContainer);
            _fpsContainer.Received().InitializeFpses(_camerasProvider.CamerasContainer.GetCamerasNames());
        }

        [Test]
        public void InternalTimerEventForFutureFramesIfHaveToAddFutureFpsTest()
        {
            // Arrange
            var challengeSettings = InitializeChallengeSettings();
            _settingsContext.ChallengeSetting.Returns(challengeSettings);
            _challengeBuffers.HaveToAddFutureFps(challengeSettings.NumberOfFutureFPS).Returns(true);
            // Act
            presenter.InternalTimerEventForFutureFrames();
            // Assert
            _challengeBuffers.Received().AddFutureFpses(_fpsContainer);
            _fpsContainer.Received().InitializeFpses(_camerasProvider.CamerasContainer.GetCamerasNames());
            _internalChallengeTimer.DidNotReceive().DisableTimerEvent();
            _challengeWriter.DidNotReceiveWithAnyArgs().WriteChallenge(null, null);
            _challengeBuffers.DidNotReceive().ClearBuffers();
            _internalChallengeTimer.DidNotReceive().EnableTimerEvent(presenter.InternalTimerEventForPastFrames);
        }

        [Test]
        public void InternalTimerEventForFutureFramesIfHaveNotToAddFutureFpsTest()
        {
            // Arrange
            var challengeSettings = InitializeChallengeSettings();
            _settingsContext.ChallengeSetting.Returns(challengeSettings);
            _challengeBuffers.HaveToAddFutureFps(challengeSettings.NumberOfFutureFPS).Returns(false);
            // Act
            presenter.InternalTimerEventForFutureFrames();
            // Assert
            _challengeBuffers.DidNotReceive().AddFutureFpses(_fpsContainer);
            _fpsContainer.Received().InitializeFpses(_camerasProvider.CamerasContainer.GetCamerasNames());
            _internalChallengeTimer.Received().DisableTimerEvent();
            _challengeWriter.Received().WriteChallenge(
                _videoContainer.ConvertToVideoContainer(null),
                                _challengeObject.PathToChallengeDirectory);
            _challengeBuffers.Received().ClearBuffers();
            _internalChallengeTimer.Received().EnableTimerEvent(presenter.InternalTimerEventForPastFrames);
        }
    }
}
