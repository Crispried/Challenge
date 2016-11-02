using NUnit.Framework;
using Challange.Presenter.Base;
using Challange.Presenter.Views;
using NSubstitute;
using Challange.Presenter.Presenters.PlayerPanelSettingsPresenter;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Message;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class PlayerPanelSettingsPresenterTest : TestCase
    {
        private IApplicationController controller;
        private PlayerPanelSettingsPresenter presenter;
        private IPlayerPanelSettingsView view;
        private PlayerPanelSettings mock;
        private PlayerPanelSettings argument;
        private ISettingsService<PlayerPanelSettings> settingService;
        private IMessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IPlayerPanelSettingsView>();
            messageParser = Substitute.For<IMessageParser>();
            settingService =
                Substitute.For<ISettingsService<PlayerPanelSettings>>();
            presenter = new PlayerPanelSettingsPresenter(
                controller, view, messageParser, settingService);
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
            // Act
            presenter.ChangePlayerPanelSettings(argument);
            // Assert
            settingService.Received().SaveSetting(argument);
            mock.Received().SetSettings(argument);
            view.Received().Close();
            var returnedMessage =
                messageParser.DidNotReceiveWithAnyArgs().GetMessage(default(MessageType));
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }


        [Test]
        public void ChangePlayerPanelSettingsIfFormIsInvalid()
        {
            // Arrange
            SetFormAsValid(false);
            // Act
            presenter.ChangePlayerPanelSettings(argument);
            // Assert
            settingService.DidNotReceiveWithAnyArgs().SaveSetting(argument);
            mock.DidNotReceiveWithAnyArgs().SetSettings(argument);
            view.DidNotReceiveWithAnyArgs().Close();
            var returnedMessage =
                messageParser.Received().GetMessage(MessageType.PlayerPanelSettingsInvalid);
            view.Received().ShowMessage(returnedMessage);
        }

        private void SetFormAsValid(bool isValid)
        {
            view.ValidateForm().Returns(isValid);
        }
    }
}
