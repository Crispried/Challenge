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
        bool timerHandlerWasInvoked;
        double interval;
        bool autoReset;

        [SetUp]
        public void SetUp()
        {
            interval = 0.1;
            autoReset = true;
            internalChallengeTimer =
                    new InternalChallengeTimer(interval, autoReset);
        }

        [Test]
        public void EnableTimerEventTest()
        {
            // Arrange
            timerHandlerWasInvoked = false;
            // Act
            TimerStart();
            internalChallengeTimer.EnableTimerEvent(TimerHandler);
            while (true)
            {
                if (timerHandlerWasInvoked)
                {
                    break;
                }
            }
            // Assert
            Assert.IsTrue(timerHandlerWasInvoked);
        }

        //[Test]
        //public void DisableTimerEventTest()
        //{
        //    // Arrange
        //    timerHandlerWasInvoked = false;
        //    // Act
        //    internalChallengeTimer.EnableTimerEvent(TimerHandler);
        //    internalChallengeTimer.DisableTimerEvent();
        //    while (timerHandlerWasInvoked)
        //    {

        //    }
        //    // Assert
        //    Assert.IsFalse(timerHandlerWasInvoked);
        //}

        private void TimerHandler()
        {
            timerHandlerWasInvoked = !timerHandlerWasInvoked;
        }

        private void TimerStart()
        {
            internalChallengeTimer.Start();
        }
    }
}
