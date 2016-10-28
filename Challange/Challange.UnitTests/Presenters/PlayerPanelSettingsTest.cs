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
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Message;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class PlayerPanelSettingsTest : TestCase
    {
        private IApplicationController controller;
        private PlayerPanelSettingsPresenter presenter;
        private IPlayerPanelSettingsView view;
        private PlayerPanelSettings mock;
        private PlayerPanelSettings argument;
        private SettingsService<PlayerPanelSettings> settingService;
        private IMessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IPlayerPanelSettingsView>();
            messageParser = Substitute.For<IMessageParser>();
            IFileWorker fileWorker = Substitute.For<IFileWorker>();
            PlayerPanelSettingsParser parser =
                Substitute.For<PlayerPanelSettingsParser>(fileWorker);
            settingService =
                Substitute.For<SettingsService<PlayerPanelSettings>>(parser);
            presenter = new PlayerPanelSettingsPresenter(
                controller, view, messageParser, fileWorker);
            mock = Substitute.For<PlayerPanelSettings>();
            argument = InitializePlayerPanelSettings();
            presenter.Run(mock);
        }

        [Test]
        public void Run()
        {
            // Arrange
            // Act
            // Assert
            view.Received().SetPlayerPanelSettings(mock);
            view.Received().Show();
        }

        [Test]
        public void ChangePlayerPanelSettingsIfFormIsValid()
        {
            // Arrange
            SetFormAsValid(true);
            var returnedMessage = new ChallengeMessage()
            {
                MessageType = MessageType.PlayerPanelSettingsInvalid
            };
            // Act
            presenter.ChangePlayerPanelSettings(argument);
            // Assert
            settingService.DidNotReceive().SaveSetting(argument);
            mock.Received().SetSettings(argument);
            view.Received().Close();
            messageParser.DidNotReceiveWithAnyArgs().GetMessage(MessageType.PlayerPanelSettingsInvalid);
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }


        [Test]
        public void ChangePlayerPanelSettingsIfFormIsInvalid()
        {
            // Arrange
            SetFormAsValid(false);
            var returnedMessage = new ChallengeMessage()
            {
                MessageType = MessageType.PlayerPanelSettingsInvalid
            };
            // Act
            presenter.ChangePlayerPanelSettings(argument);
            // Assert
            settingService.DidNotReceiveWithAnyArgs().SaveSetting(argument);
            mock.DidNotReceiveWithAnyArgs().SetSettings(argument);
            view.DidNotReceiveWithAnyArgs().Close();
            messageParser.Received().GetMessage(MessageType.PlayerPanelSettingsInvalid);
            view.Received().ShowMessage(returnedMessage);
        }

        private void SetFormAsValid(bool isValid)
        {
            view.ValidateForm().Returns(isValid);
        }
    }
}
