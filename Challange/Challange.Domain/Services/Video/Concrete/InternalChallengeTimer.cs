using Challange.Domain.Entities;
using Challange.Domain.Services.Event;
using Challange.Domain.Services.Video.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Challange.Domain.Servuces.Video.Concrete
{
    public class InternalChallengeTimer : IInternalChallengeTimer
    {
        private Timer timer;
        private Delegate timerElapsedEventHandler;
        private IEventSubscriber eventSubscriber;

        public InternalChallengeTimer(IEventSubscriber eventSubscriber)
        {
            timer = new Timer(1000);
            timer.AutoReset = true;
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
