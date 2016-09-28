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
            timer = new Timer(10);
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
            timer = new Timer(1);
            timer.Start();
            // Act
            var timerEventHandler = EventSubscriber.AddEventHandler(timer,
                    "Elapsed", TimerElapsed);
            for (int i = 0; i < 10000000; i++)
            {
                if (eventWasRaised)
                {
                    break;
                }
            }
            EventSubscriber.RemoveEventHandler(timer, "Elapsed", timerEventHandler);
            eventWasRaised = false;
            for (int i = 0; i < 100000; i++)
            {
                if (eventWasRaised)
                {
                    break;
                }
            }
            // Assert
            Assert.False(eventWasRaised);
        }

        private void TimerElapsed()
        {
            eventWasRaised = true;
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
