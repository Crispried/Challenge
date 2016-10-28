using Challange.Domain.Entities;
using Challange.Domain.Infrastructure;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.FtpSettingsPresenter;
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
    class FtpPresenterTest : TestCase
    {
        private IApplicationController controller;
        private FtpSettingsPresenter presenter;
        private IFtpSettingsView view;
        private FtpSettings argument;
        private FtpSettings mock;
        private SettingsService<FtpSettings> ftpSettingsService;
        private FtpConnector ftpConnector;
        private IMessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IFtpSettingsView>();
            messageParser = Substitute.For<IMessageParser>();
            presenter = new FtpSettingsPresenter(controller, view, messageParser);
            mock = Substitute.For<FtpSettings>();
            argument = InitializeFtpSettings();
            IFileWorker fileWorker = Substitute.For<IFileWorker>();
            FtpSettingsParser parser =
                Substitute.For<FtpSettingsParser>(fileWorker);
            ftpSettingsService =
                Substitute.For<SettingsService<FtpSettings>>(parser);
            presenter.Run(mock);
        }

        [Test]
        public void Run()
        {
            // Arrange
            // Act
            // Assert
            view.SetFtpSettings(mock);
            view.Show();
        }

        [Test]
        public void ChangeFtpSettingsIfFormIsValid()
        {
            // Arrange
            SetFormAsValid(true);
            var returnedMessage = new ChallengeMessage()
            {
                MessageType = MessageType.FtpSettingsInvalid
            };
            // Act
            // Assert
            presenter.ChangeFtpSettings(argument);
            ftpSettingsService.DidNotReceive().SaveSetting(argument);
            mock.Received().SetSettings(argument);
            view.Received().Close();
            messageParser.DidNotReceiveWithAnyArgs().GetMessage(MessageType.FtpSettingsInvalid);
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }

        [Test]
        public void ChangeFtpSettingsIfFormIsInvalid()
        {
            // Arrange
            SetFormAsValid(false);
            var returnedMessage = new ChallengeMessage()
            {
                MessageType = MessageType.FtpSettingsInvalid
            };
            // Act
            // Assert
            presenter.ChangeFtpSettings(argument);
            ftpSettingsService.DidNotReceiveWithAnyArgs().SaveSetting(argument);
            mock.DidNotReceiveWithAnyArgs().SetSettings(argument);
            view.DidNotReceive().Close();
            messageParser.Received().GetMessage(MessageType.FtpSettingsInvalid);
            view.ReceivedWithAnyArgs().ShowMessage(returnedMessage);
        }

        [Test]
        public void TestFtpConnectionSuccess()
        {
            // Arrange
            ftpConnector = Substitute.For<FtpConnector>(argument.FtpAddress,
                                argument.UserName, argument.Password);
            var returnedMessage = new ChallengeMessage()
            {
                MessageType = MessageType.TestFtpConnectionSuccessed
            };
            // Act
            // Assert
            presenter.TestFtpConnection(argument);
            ftpConnector.Received().IsFtpConnectionSuccessful();
            messageParser.Received().GetMessage(MessageType.TestFtpConnectionSuccessed);
            view.ReceivedWithAnyArgs().ShowMessage(returnedMessage);
        }

        private void SetFormAsValid(bool isValid)
        {
            view.ValidateForm().Returns(isValid);
        }
    }
}
