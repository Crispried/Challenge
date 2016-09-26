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
        private bool eventWasRaised;
        private Delegate timerEventHandler;

        [SetUp]
        public void SetUp()
        {
            timer = new Timer(1);
            timer.Start();
        }

        [Test]
        public void SubscribeToEvent()
        {
            // Arrange
            eventWasRaised = false;
            // Act
            AddEventHandler();
            // Assert
            while (true)
            {
                if (eventWasRaised)
                {
                    break;
                }
            }
            Assert.True(eventWasRaised);
        }

        [Test]
        public void UnsubscribeFromEvent()
        {
            // Arrange
            eventWasRaised = false;
            // Act
            AddEventHandler();
            while (true)
            {
                if (eventWasRaised)
                {
                    break;
                }
            }
            RemoveEventHandler();
            // Assert
            while (true)
            {
                if (!eventWasRaised)
                {
                    break;
                }
            }
            Assert.False(eventWasRaised);
        }

        private void TimerElapsed()
        {
            eventWasRaised = !eventWasRaised;
        }

        private void AddEventHandler()
        {
            timerEventHandler = EventSubscriber.AddEventHandler(timer,
                                "Elapsed", TimerElapsed);
        }

        private void RemoveEventHandler()
        {
            EventSubscriber.RemoveEventHandler(timer, "Elapsed", timerEventHandler);
        }
    }
}
