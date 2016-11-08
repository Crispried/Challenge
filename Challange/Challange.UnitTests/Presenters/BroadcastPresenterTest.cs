using Challange.Domain.Entities;
using Challange.Domain.Services.Event;
using Challange.Domain.Services.StreamProcess.Concrete.Pylon;
using Challange.Presenter.Base;
using Challange.Presenter.Presenters.BroadcastPresenter;
using Challange.Presenter.Presenters.CamerasPresenter;
using Challange.Presenter.Views;
using Moq;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challange.UnitTests.Presenters
{
    [TestFixture]
    class BroadcastPresenterTest
    {
        private IApplicationController controller;
        private BroadcastPresenter presenter;
        private IBroadcastView view;
        private Camera argument;
        private IEventSubscriber eventSubscriber;

        [SetUp]
        public void SetUp()
        {
            controller = Substitute.For<IApplicationController>();
            view = Substitute.For<IBroadcastView>();
            eventSubscriber = Substitute.For<IEventSubscriber>();
            presenter = new BroadcastPresenter(controller, view, eventSubscriber);
            argument = new PylonCamera(0, "test");
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
