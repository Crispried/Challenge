using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Challange.Domain.Entities;
using NUnit.Framework;
using System.Timers;
using System.Reflection;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class EventSubscriberTest
    {
        private Timer timer;
        private EventInfo eventInfo;
        private bool eventWasRaised = false;

        [SetUp]
        public void SetUp()
        {
            timer = new Timer();
            eventInfo = typeof(Timer).GetEvent("Elapsed");
        }

        [Test]
        public void SubscribeToEventSubscribes()
        {
            // Arrange

            // Act
            AddEventHandler();

            // Assert
            Assert.True(eventWasRaised);
        }

        [Test]
        public void RemoveEventHandlerUnsubscribes()
        {
            // Arrange

            // Act
            RemoveEventHandler();

            // Assert
            Assert.False(eventWasRaised);
        }

        private void TimerElapsed()
        {
            eventWasRaised = !eventWasRaised;
        }

        private void AddEventHandler()
        {
            EventSubscriber.AddEventHandler(eventInfo, timer, TimerElapsed);
        }

        private void RemoveEventHandler()
        {
            EventSubscriber.RemoveEventHandler(eventInfo, timer, TimerElapsed);
        }
    }
}
