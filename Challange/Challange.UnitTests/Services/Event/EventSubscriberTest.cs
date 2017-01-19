using System;
using NUnit.Framework;
using System.Threading;
using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.Event.Concrete;

namespace Challange.UnitTests.Services.Event
{
    [TestFixture]
    class EventSubscriberTest : TestCase
    {
        private System.Timers.Timer timer;
        private bool eventWasRaised;
        private double interval;
        private IEventSubscriber eventSubscriber;

        [SetUp]
        public void SetUp()
        {
            eventSubscriber = new EventSubscriber();
            interval = 100;
            timer = new System.Timers.Timer(interval);
            timer.Start();
        }

        [Test]
        public void SubscribeToEvent()
        {
            // Arrange
            eventWasRaised = false;
            // Act
            var handler = AddEventHandler();
            // Assert
            Thread.Sleep(Convert.ToInt16(interval * 2));
            Assert.True(eventWasRaised);
            timer.Enabled = false;
        }

        [Test]
        public void UnsubscribeFromEvent()
        {
            // Arrange           
            // Act
            var handler = AddEventHandler();
            RemoveEventHandler(handler);
            eventWasRaised = false;
            Thread.Sleep(Convert.ToInt16(interval*2));
            // Assert
            Assert.False(eventWasRaised);
        }

        [Test]
        public void SubscribeToEventUsingHandlerWithArgs()
        {
            // Arrange
            eventWasRaised = false;
            // Act
            var handler = AddEventHandlerWithParameters();
            // Assert
            Thread.Sleep(Convert.ToInt16(interval * 2));
            Assert.True(eventWasRaised);
            timer.Enabled = false;
        }

        private void TimerElapsedWithParameters(object obj, EventArgs e)
        {
            eventWasRaised = true;
        }

        private void TimerElapsed()
        {
            eventWasRaised = true;
        }

        private Delegate AddEventHandler()
        {
            var timerEventHandler = eventSubscriber.AddEventHandler(timer,
                                "Elapsed", TimerElapsed);
            return timerEventHandler;
        }

        private Delegate AddEventHandlerWithParameters()
        {
            var timerEventHandler = eventSubscriber.AddEventHandler(timer,
                                "Elapsed", TimerElapsedWithParameters);
            return timerEventHandler;
        }

        private void RemoveEventHandler(Delegate handler)
        {
            eventSubscriber.RemoveEventHandler(timer, "Elapsed", handler);
        }
    }
}
