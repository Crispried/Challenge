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

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class ChallengeSettingsPresenterTest
    {
        private IApplicationController controller;
        private ChallengeSettingsPresenter presenter;
        private IChallengeSettingsView view;
        private ChallengeSettings argument;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IChallengeSettingsView>();
            presenter = new ChallengeSettingsPresenter(controller, view);
            argument = InitializeChallengeSettings();
            presenter.Run(argument);
        }

        [Test]
        public void Run()
        {
            // Arrange

            // Act
            presenter.Run(argument);

            // Assert
            Assert.True(presenter.ChallengeSettingsAreOpened);
        }

        [Test]
        public void ChangeChallengeSettingsValidForm()
        {
            // Arrange
            view.ChallengeSettings = argument;
            SetFormAsValid(true);

            // Act
            view.ChangeChallengeSettings += Raise.Event<Action>();

            // Assert
            Assert.True(presenter.ChallengeSettingsFormIsValid);
        }

        [Test]
        public void ChangeChallengeSettingsInvalidForm()
        {
            // Arrange
            view.ChallengeSettings = argument;
            SetFormAsValid(false);

            // Act
            view.ChangeChallengeSettings += Raise.Event<Action>();

            // Assert
            Assert.False(presenter.ChallengeSettingsFormIsValid);
        }

        [Test]
        public void ChangeChallengeSettingsSaveSettings()
        {
            // Arrange
            view.ChallengeSettings = argument;
            SetFormAsValid(true);

            // Act
            view.ChangeChallengeSettings += Raise.Event<Action>();

            // Assert
            Assert.True(presenter.ChallengeSettingsAreSaved);
        }

        private void SetFormAsValid(bool isValid)
        {
            view.ValidateForm().Returns(isValid);
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
