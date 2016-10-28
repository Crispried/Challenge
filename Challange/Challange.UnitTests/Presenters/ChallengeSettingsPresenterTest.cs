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
using Challange.Domain.Infrastructure;
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
        private SettingsService<ChallengeSettings> challengeSettingsService;
        private IMessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IChallengeSettingsView>();
            messageParser = Substitute.For<IMessageParser>();
            presenter = new ChallengeSettingsPresenter(controller, view, messageParser);
            mock = Substitute.For<ChallengeSettings>();
            IFileWorker fileWorker = Substitute.For<IFileWorker>();
            ChallengeSettingsParser parser = 
                Substitute.For<ChallengeSettingsParser>(fileWorker);
            challengeSettingsService =
                Substitute.For<SettingsService<ChallengeSettings>> (parser);
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
            var returnedMessage = new ChallengeMessage()
            {
                MessageType = MessageType.ChallengeSettingsInvalid
            };
            // Act
            // Assert
            presenter.ChangeChallengeSettings(argument);
            challengeSettingsService.Received().SaveSetting(argument);
            mock.Received().SetSettings(argument);
            view.Received().Close();
            messageParser.DidNotReceive().GetMessage(MessageType.ChallengeSettingsInvalid);
            view.DidNotReceive().ShowMessage(returnedMessage);
        }

        [Test]
        public void ChangeChallengeSettingsIfFormIsInvalid()
        {
            // Arrange
            SetFormAsValid(false);
            var returnedMessage = new ChallengeMessage()
            {
                MessageType = MessageType.ChallengeSettingsInvalid
            };
            // Act
            // Assert
            presenter.ChangeChallengeSettings(argument);
            challengeSettingsService.DidNotReceiveWithAnyArgs().SaveSetting(argument);
            mock.DidNotReceiveWithAnyArgs().SetSettings(argument);
            view.DidNotReceive().Close();
            messageParser.Received().GetMessage(MessageType.ChallengeSettingsInvalid);
            view.ReceivedWithAnyArgs().ShowMessage(returnedMessage);
            
        }

        private void SetFormAsValid(bool isValid)
        {
            view.ValidateForm().Returns(isValid);
        }
    }
}
