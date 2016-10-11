using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Challange.Domain.Entities
{
    public class InternalChallengeTimer
    {
        private Timer timer;
        private Delegate timerElapsedEventHandler;

        public InternalChallengeTimer(double interval, bool autoReset)
        {
            timer = new Timer(interval);
            timer.AutoReset = autoReset;
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
            timerElapsedEventHandler = EventSubscriber.AddEventHandler
                    (timer, "Elapsed", action);
        }

        public void DisableTimerEvent()
        {
            EventSubscriber.RemoveEventHandler(timer, "Elapsed",
                timerElapsedEventHandler);
        }
    }
}
