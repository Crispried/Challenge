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
    class FtpSettingsPresenterTest : TestCase
    {
        private IApplicationController controller;
        private FtpSettingsPresenter presenter;
        private IFtpSettingsView view;
        private FtpSettings argument;
        private FtpSettings mock;
        private ISettingsService<FtpSettings> settingsSerivce;
        private IMessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IFtpSettingsView>();
            messageParser = Substitute.For<IMessageParser>();
            settingsSerivce =
                Substitute.For<ISettingsService<FtpSettings>>();
            presenter = new FtpSettingsPresenter(controller, view, messageParser, settingsSerivce);
            mock = Substitute.For<FtpSettings>();
            argument = InitializeFtpSettings();
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
            // Act
            // Assert
            presenter.ChangeFtpSettings(argument);
            settingsSerivce.Received().SaveSetting(argument);
            mock.Received().SetSettings(argument);
            view.Received().Close();
            var returnedMessage = 
                messageParser.DidNotReceiveWithAnyArgs().GetMessage(default(MessageType));
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }

        [Test]
        public void ChangeFtpSettingsIfFormIsInvalid()
        {
            // Arrange
            SetFormAsValid(false);
            // Act
            // Assert
            presenter.ChangeFtpSettings(argument);
            settingsSerivce.DidNotReceiveWithAnyArgs().SaveSetting(argument);
            mock.DidNotReceiveWithAnyArgs().SetSettings(argument);
            view.DidNotReceive().Close();
            var returnedMessage = 
                messageParser.Received().GetMessage(MessageType.FtpSettingsInvalid);
            view.ReceivedWithAnyArgs().ShowMessage(returnedMessage);
        }

        [Test]
        public void TestFtpConnectionSuccess()
        {
            // Arrange
            var ftpConnector = Substitute.For<FtpConnector>(argument.FtpAddress,
                                argument.UserName, argument.Password);
            // Act
            // Assert
            presenter.TestFtpConnection(argument);
            ftpConnector.Received().IsFtpConnectionSuccessful();
            var returnedMessage = 
                messageParser.Received().GetMessage(MessageType.TestFtpConnectionSuccessed);
            view.Received().ShowMessage(returnedMessage);
        }

        [Test]
        public void TestFtpConnectionFailed()
        {
            // Arrange
            var ftpConnector = Substitute.For<FtpConnector>(null,
                                null, null);
            // Act
            // Assert
            presenter.TestFtpConnection(mock);
            ftpConnector.Received().IsFtpConnectionSuccessful();
            var returnedMessage =
                messageParser.Received().GetMessage(MessageType.TestFtpConnectionFailed);
            view.Received().ShowMessage(returnedMessage);
        }

        private void SetFormAsValid(bool isValid)
        {
            view.ValidateForm().Returns(isValid);
        }
    }
}
