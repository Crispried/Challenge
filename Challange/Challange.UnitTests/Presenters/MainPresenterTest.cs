using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.ChallengeSettingsPresenter;
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
        private PlayerPanelSettings playerPanelSettings;
        private ChallengeSettings challengeSettings;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IMainView>();
            presenter = new MainPresenter(controller, view);
            presenter.Run();
            playerPanelSettings = InitializePlayerPanelSettings();
            challengeSettings = InitializeChallengeSettings();
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
            presenter.ChallengeSettings = InitializeChallengeSettings();
            view.CreateChallange += Raise.Event<Action>();
            // Assert
            Assert.IsTrue(presenter.ElapsedTimeWasGot);
            Assert.IsTrue(presenter.DirectoryForChallengeWasCreated);
            Assert.IsFalse(presenter.IsEventForPastFramesActive);
            Assert.IsTrue(presenter.IsEventForFutureFramesActive);
            Assert.IsFalse(presenter.IsChallengeButtonEnable);
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
