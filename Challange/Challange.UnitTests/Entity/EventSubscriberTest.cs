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

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class EventSubscriberTest
    {
        private System.Timers.Timer timer;
        private bool eventWasRaised;
        private double interval;

        [SetUp]
        public void SetUp()
        {
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

        private void TimerElapsed()
        {
            eventWasRaised = true;
        }

        private Delegate AddEventHandler()
        {
            var timerEventHandler = EventSubscriber.AddEventHandler(timer,
                                "Elapsed", TimerElapsed);
            return timerEventHandler;
        }

        private void RemoveEventHandler(Delegate handler)
        {
            EventSubscriber.RemoveEventHandler(timer, "Elapsed", handler);
        }
    }
}
