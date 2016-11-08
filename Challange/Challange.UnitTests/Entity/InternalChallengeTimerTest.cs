using Challange.Domain.Entities;
using Challange.Domain.Servuces.Video.Concrete;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challange.UnitTests.Entity
{
    [TestFixture]
    class InternalChallengeTimerTest
    {
        private InternalChallengeTimer internalChallengeTimer;
        private bool timerHandlerWasInvoked;
        private double interval;
        private bool autoReset;

        [SetUp]
        public void SetUp()
        {
            interval = 100;
            autoReset = true;
            internalChallengeTimer =
                    new InternalChallengeTimer(interval, autoReset);
            internalChallengeTimer.Start();
        }

        [Test]
        public void EnableTimerEventTest()
        {
            // Arrange
            timerHandlerWasInvoked = false;
            // Act
            internalChallengeTimer.EnableTimerEvent(TimerHandler);
            // Assert
            Thread.Sleep(Convert.ToInt16(interval * 2));
            Assert.True(timerHandlerWasInvoked);
            internalChallengeTimer.Timer.Enabled = false;
        }

        [Test]
        public void DisableTimerEventTest()
        {
            // Arrange           
            // Act
            internalChallengeTimer.EnableTimerEvent(TimerHandler);
            internalChallengeTimer.DisableTimerEvent();
            timerHandlerWasInvoked = false;
            Thread.Sleep(Convert.ToInt16(interval * 2));
            // Assert
            Assert.False(timerHandlerWasInvoked);
        }

        private void TimerHandler()
        {
            timerHandlerWasInvoked = true;
        }

        private void TimerStart()
        {
            internalChallengeTimer.Start();
        }
    }
}
