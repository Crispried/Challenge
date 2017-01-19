using Challange.Domain.Services.Ftp.Concrete;
using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.FtpSettingsPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using NUnit.Framework;

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
        private ISettingsContext settingsContext;
        private IMessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IFtpSettingsView>();
            messageParser = Substitute.For<IMessageParser>();
            settingsContext =
                Substitute.For<ISettingsContext>();
            presenter = new FtpSettingsPresenter(controller, view, messageParser, settingsContext);
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
            settingsContext.Received().SaveFtpSetting(argument);
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
            settingsContext.DidNotReceiveWithAnyArgs().SaveFtpSetting(argument);
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
            var ftpConnector = Substitute.For<FtpWorker>(argument.FtpAddress,
                                argument.UserName, argument.Password);
            // Act
            presenter.TestFtpConnection(argument);
            // Assert
            ftpConnector.Received().IsFtpConnectionSuccessful();
            var returnedMessage = 
                messageParser.Received().GetMessage(MessageType.TestFtpConnectionSuccessed);
            view.Received().ShowMessage(returnedMessage);
        }

        [Test]
        public void TestFtpConnectionFailed()
        {
            // Arrange
            var ftpConnector = Substitute.For<FtpWorker>(null,
                                null, null);
            // Act
            presenter.TestFtpConnection(mock);
            // Assert
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
