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
using Challange.Presenter.Presenters.ChallengeSettingsPresenter;
using Challange.Domain.Services.Settings.SettingTypes;
using Moq;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Message;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class ChallengeSettingsPresenterTest : TestCase
    {
        private IApplicationController controller;
        private ChallengeSettingsPresenter presenter;
        private IChallengeSettingsView view;
        private ChallengeSettings argument;
        private ChallengeSettings mock;
        private ISettingsService<ChallengeSettings> settingsService;
        private IMessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IChallengeSettingsView>();
            messageParser = Substitute.For<IMessageParser>();
            settingsService =
                Substitute.For<ISettingsService<ChallengeSettings>>();
            presenter = new ChallengeSettingsPresenter(controller, view, messageParser, settingsService);
            mock = Substitute.For<ChallengeSettings>();
            argument = InitializeChallengeSettings();
            presenter.Run(mock);
        }

        [Test]
        public void Run()
        {
            // Arrange
            // Act
            // Assert
            view.Received().SetChallengeSettings(mock);
            view.Received().Show();
        }

        [Test]
        public void ChangeChallengeSettingsIfFormIsValid()
        {
            // Arrange
            SetFormAsValid(true);
            // Act
            // Assert
            presenter.ChangeChallengeSettings(argument);
            settingsService.Received().SaveSetting(argument);
            mock.Received().SetSettings(argument);
            view.Received().Close();
            var returnedMessage = 
                messageParser.DidNotReceiveWithAnyArgs().GetMessage(default(MessageType));
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }

        [Test]
        public void ChangeChallengeSettingsIfFormIsInvalid()
        {
            // Arrange
            SetFormAsValid(false);
            // Act
            // Assert
            presenter.ChangeChallengeSettings(argument);
            settingsService.DidNotReceiveWithAnyArgs().SaveSetting(argument);
            mock.DidNotReceiveWithAnyArgs().SetSettings(argument);
            view.DidNotReceive().Close();
            var returnedMessage = 
                messageParser.Received().GetMessage(MessageType.ChallengeSettingsInvalid);
            view.Received().ShowMessage(returnedMessage);
            
        }

        private void SetFormAsValid(bool isValid)
        {
            view.ValidateForm().Returns(isValid);
        }
    }
}
