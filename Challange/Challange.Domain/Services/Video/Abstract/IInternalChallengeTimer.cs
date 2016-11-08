using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Video.Abstract
{
    public interface IInternalChallengeTimer
    {
        void EnableTimerEvent(Action action);

        void DisableTimerEvent();

        void Start();
    }
}
