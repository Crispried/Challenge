using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.Video.Abstract;
using Challange.Domain.Services.Video.Concrete;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Challange.UnitTests.Services.Video
{
    [TestFixture]
    class InternalChallengeTimerTest : TestCase
    {
        private IInternalChallengeTimer internalChallengeTimer;
        private IEventSubscriber eventSubscriber;
        private System.Timers.Timer timer;
        private Delegate eventHandler;

        [SetUp]
        public void SetUp()
        {
            eventSubscriber = Substitute.For<IEventSubscriber>();
            timer = Substitute.For<System.Timers.Timer>();
            internalChallengeTimer = new InternalChallengeTimer(timer, eventSubscriber);
            internalChallengeTimer.Start();
        }

        [Test]
        public void TimerElapsedEventHandler()
        {
            // Arramge
            var testDelegate = Substitute.For<TestDelegate>();
            // Act
            internalChallengeTimer.TimerElapsedEventHandler = testDelegate;
            var getter = internalChallengeTimer.TimerElapsedEventHandler;
            // Assert
            Assert.IsTrue(testDelegate.Equals(getter));
            Assert.IsTrue(testDelegate.Equals(internalChallengeTimer.TimerElapsedEventHandler));
        }

        [Test]
        public void TestElapsedEventHandlerPropertyProperty()
        {
            // Arrange
            var tempTimer = Substitute.For<IInternalChallengeTimer>();
            // Act
            var del = Substitute.For<TestDelegate>();
            tempTimer.TimerElapsedEventHandler = del;
            var call = tempTimer.TimerElapsedEventHandler;
            // Assert
            var test = tempTimer.Received().TimerElapsedEventHandler;
            tempTimer.Received().TimerElapsedEventHandler = del;
        }

        [Test]
        public void TestTimerProperty()
        {
            // Arrange
            // Act
            var getter = internalChallengeTimer.Timer;
            // Assert
            Assert.IsTrue(getter == internalChallengeTimer.Timer);
        }

        [Test]
        public void StartTest()
        {
            // Arrage
            // Act
            internalChallengeTimer.Start();
            // Assert
            timer.Received().Start();
        }

        [Test]
        public void EnableTimerEventTest()
        {
            // Arrange
            // Act
            internalChallengeTimer.EnableTimerEvent(TimerHandler);
            // Assert
            eventHandler = eventSubscriber.Received().AddEventHandler(timer, "Elapsed", TimerHandler);
        }

        [Test]
        public void DisableTimerEventTest()
        {
            // Arrange           
            // Act
            internalChallengeTimer.DisableTimerEvent();
            // Assert
             eventSubscriber.Received().RemoveEventHandler(timer, "Elapsed", eventHandler);
        }

        [ExcludeFromCodeCoverage]
        private void TimerHandler()
        {

        }
    }
}
