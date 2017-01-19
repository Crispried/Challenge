using System;

namespace Challange.Domain.Services.Event.Abstract
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
