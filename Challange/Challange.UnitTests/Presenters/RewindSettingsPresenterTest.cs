using Challange.Domain.Services.Message;
using Challange.Domain.Services.Settings;
using Challange.Domain.Services.Settings.SettingParser;
using Challange.Domain.Services.Settings.SettingTypes;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.RewindSettingsPresenter;
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
    class RewindSettingsPresenterTest : TestCase
    {
        private IApplicationController controller;
        private RewindSettingsPresenter presenter;
        private IRewindSettingsView view;
        private RewindSettings mock;
        private RewindSettings argument;
        private ISettingsContext settingsContext;
        private IMessageParser messageParser;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IRewindSettingsView>();
            messageParser = Substitute.For<IMessageParser>();
            settingsContext =
                Substitute.For<ISettingsContext>();
            presenter = new RewindSettingsPresenter(
                controller, view, messageParser, settingsContext);
            mock = Substitute.For<RewindSettings>();
            argument = InitializeRewindSettings();
            presenter.Run(mock);
        }

        [Test]
        public void Run()
        {
            // Arrange
            // Act
            // Assert
            view.Received().SetRewindSettings(mock);
            view.Received().Show();
        }

        [Test]
        public void ChangeRewindSettingsIfFormIsValid()
        {
            // Arrange
            SetFormAsValid(true);
            // Act
            presenter.ChangeRewindSettings(argument);
            // Assert
            settingsContext.DidNotReceiveWithAnyArgs().SaveRewindSetting(argument);
            mock.Received().SetSettings(argument);
            view.Received().Close();
            var returnedMessage = 
                messageParser.DidNotReceiveWithAnyArgs().GetMessage(default(MessageType));
            view.DidNotReceiveWithAnyArgs().ShowMessage(returnedMessage);
        }


        [Test]
        public void ChangeRewindSettingsIfFormIsInvalid()
        {
            // Arrange
            SetFormAsValid(false);
            // Act
            presenter.ChangeRewindSettings(argument);
            // Assert
            settingsContext.DidNotReceiveWithAnyArgs().SaveRewindSetting(argument);
            mock.DidNotReceiveWithAnyArgs().SetSettings(argument);
            view.DidNotReceiveWithAnyArgs().Close();
            var returnedMessage =
                messageParser.Received().GetMessage(MessageType.RewindSettingsInvalid);
            view.Received().ShowMessage(returnedMessage);
        }

        private void SetFormAsValid(bool isValid)
        {
            view.ValidateForm().Returns(isValid);
        }
    }
}
