using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.StreamProcess.Abstract;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.BroadcastPresenter;
using Challange.Presenter.Views;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class BroadcastPresenterTest : TestCase
    {
        private IApplicationController controller;
        private BroadcastPresenter presenter;
        private IBroadcastView view;
        private ICamera argument;
        private IEventSubscriber eventSubscriber;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IBroadcastView>();
            eventSubscriber = Substitute.For<IEventSubscriber>();
            presenter = new BroadcastPresenter(controller, view, eventSubscriber);
            argument = Substitute.For<ICamera>();
            presenter.Run(argument);
        }

        [Test]
        public void Run()
        {
            // Arrange
            // Act
            // Assert
            view.Received().Show();
        }

        [Test]
        public void BroadcastShowCallback()
        {
            // Arrange
            bool eventWasRaised = false;
            // Act
            view.BroadcastShowCallback += delegate
            {
                eventWasRaised = true;
            };
            view.BroadcastShowCallback += Raise.Event<Action>();
            // Assert
            Assert.IsTrue(eventWasRaised);
        }
    }
}
