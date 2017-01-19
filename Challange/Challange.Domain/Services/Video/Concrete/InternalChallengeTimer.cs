using Challange.Domain.Services.Event.Abstract;
using Challange.Domain.Services.Video.Abstract;
using System;
using System.Timers;

namespace Challange.Domain.Services.Video.Concrete
{
    public class InternalChallengeTimer : IInternalChallengeTimer
    {
        private Timer timer;
        private Delegate timerElapsedEventHandler;
        private IEventSubscriber eventSubscriber;

        public InternalChallengeTimer(Timer timer, IEventSubscriber eventSubscriber)
        {
            this.timer = timer;
            this.eventSubscriber = eventSubscriber;
        }

        public Delegate TimerElapsedEventHandler
        {
            get
            {
                return timerElapsedEventHandler;
            }
            set
            {
                timerElapsedEventHandler = value;
            }
        }

        public Timer Timer
        {
            get
            {
                return timer;
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void EnableTimerEvent(Action action)
        {
            timerElapsedEventHandler = eventSubscriber.AddEventHandler
                    (timer, "Elapsed", action);
        }

        public void DisableTimerEvent()
        {
            eventSubscriber.RemoveEventHandler(timer, "Elapsed",
                timerElapsedEventHandler);
        }
    }
}
