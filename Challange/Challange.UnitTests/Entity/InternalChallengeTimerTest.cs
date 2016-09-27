using Challange.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class InternalChallengeTimerTest
    {
        InternalChallengeTimer internalChallengeTimer;
        bool TimerHandlerWasInvoked;

        [SetUp]
        public void SetUp()
        {
            double interval = 5;
            bool autoReset = true;
            internalChallengeTimer =
                    new InternalChallengeTimer(interval, autoReset);
        }

        [Test]
        public void TimerNotEnabledTillStartTest()
        {
            // Arrange
            // Act
            // Assert
            Assert.IsFalse(internalChallengeTimer.Timer.Enabled);
        }

        [Test]
        public void TimerEnabledAfterStartTest()
        {
            // Arrange
            double interval = 5;
            bool autoReset = true;
            // Act
            internalChallengeTimer =
                new InternalChallengeTimer(interval, autoReset);
            internalChallengeTimer.Start();
            // Assert
            Assert.IsTrue(internalChallengeTimer.Timer.Enabled);
        }

        [Test]
        public void EnableTimerEventTest()
        {
            // Arrange
            // Act
            internalChallengeTimer.EnableTimerEvent(TimerHandler);
            while (true)
            {
                if (TimerHandlerWasInvoked)
                {
                    break;
                }
            }
            // Assert
            Assert.IsTrue(TimerHandlerWasInvoked);
        }

        [Test]
        public void DisableTimerEventTest()
        {
            // Arrange
            // Act
            internalChallengeTimer.EnableTimerEvent(TimerHandler);
            while (true)
            {
                if (TimerHandlerWasInvoked)
                {
                    break;
                }
            }
            internalChallengeTimer.DisableTimerEvent();
            while (true)
            {
                if (!TimerHandlerWasInvoked)
                {
                    break;
                }
            }
            // Assert
            Assert.IsFalse(TimerHandlerWasInvoked);
        }

        private void TimerHandler()
        {
            TimerHandlerWasInvoked = true;
        }
    }
}
