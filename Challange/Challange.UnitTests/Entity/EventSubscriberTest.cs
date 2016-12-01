using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using NUnit.Framework;
using System.Timers;
using System.Reflection;
using System.Threading;
using Challange.Domain.Services.Event;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class EventSubscriberTest : TestCase
    {
        private System.Timers.Timer timer;
        private bool eventWasRaised;
        private double interval;
        private EventSubscriber eventSubscriber;

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
