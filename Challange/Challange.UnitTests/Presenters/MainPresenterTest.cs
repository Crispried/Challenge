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

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class MainPresenterTest
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
        }

        [Test]
        public void StreamStop()
        {
            // Arrange
            // Act
            view.StopStream += Raise.Event<Action>();
            // Assert
            Assert.IsFalse(presenter.IsCaptureDevicesEnable);
            Assert.IsTrue(presenter.IsTimeAxisResetted);
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
            view.CreateChallange += Raise.Event<Action>();
            // Assert
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

        private PlayerPanelSettings InitializePlayerPanelSettings()
        {
            return new PlayerPanelSettings()
            {
                AutosizeMode = false,
                NumberOfPlayers = 5,
                PlayerHeight = 480,
                PlayerWidth = 640
            };
        }

        private ChallengeSettings InitializeChallengeSettings()
        {
            return new ChallengeSettings()
            {
                NumberOfFutureFPS = 10,
                NumberOfPastFPS = 15
            };
        }
    }
}
