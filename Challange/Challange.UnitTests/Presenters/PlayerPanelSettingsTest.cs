using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.MainPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using Challange.Presenter.Presenters.PlayerPanelSettingsPresenter;
using Challange.Domain.Services.Settings.SettingTypes;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class PlayerPanelSettingsTest
    {
        private IApplicationController controller;
        private PlayerPanelSettingsPresenter presenter;
        private IPlayerPanelSettingsView view;
        private PlayerPanelSettings argument;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IPlayerPanelSettingsView>();
            presenter = new PlayerPanelSettingsPresenter(controller, view);
            argument = InitializePlayerPanelSettings();
            presenter.Run(argument);
        }

        [Test]
        public void ChangePlayerPanelSettings()
        {
            // Arrange
            view.PlayerPanelSettings = argument;

            // Act
            view.ChangePlayerPanelSettings += Raise.Event<Action>();

            // Assert
            Assert.True(presenter.PlayerPanelSettingsAreSaved);
        }

        [Test]
        public void Run()
        {
            // Arrange

            // Act
            presenter.Run(argument);

            // Assert
            Assert.True(presenter.PlayerPanelSettingsAreOpened);
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
    }
}
