using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Domain.Services.Event
{
    public interface IEventSubscriber
    {
        Delegate AddEventHandler(object item,
                            string eventName, Action action);
        Delegate AddEventHandler(object item, string eventName, Action<object, EventArgs> action);
        void RemoveEventHandler(object item, string eventName,
                                        Delegate handler);
    }
}
