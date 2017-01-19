using Challange.Domain.Services.Event.Abstract;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Challange.Domain.Services.Event.Concrete
{
    public class EventSubscriber : IEventSubscriber
    {
        public Delegate AddEventHandler(object item,
                            string eventName, Action action)
        {
            var eventInfo = GetEventInfo(item.GetType(), eventName);
            var parameters = GetParameters(eventInfo);
            var handler = GetHandler(eventInfo, action, parameters);
            eventInfo.AddEventHandler(item, handler);
            return handler;
        }

        public Delegate AddEventHandler(object item, string eventName, Action<object, EventArgs> action)
        {
            var eventInfo = GetEventInfo(item.GetType(), eventName);
            var parameters = GetParameters(eventInfo);
            var invoke = action.GetType().GetMethod("Invoke");
            var handler = GetHandler(eventInfo, action, invoke, parameters);
            eventInfo.AddEventHandler(item, handler);
            return handler;
        }

        public void RemoveEventHandler(object item, string eventName,
                                        Delegate handler)
        {
            var eventInfo = GetEventInfo(item.GetType(), eventName);
            eventInfo.RemoveEventHandler(item, handler);
        }

        private EventInfo GetEventInfo(Type type, string eventName)
        {
            return type.GetEvent(eventName);
        }

        private ParameterExpression[] GetParameters(EventInfo eventInfo)
        {
            return eventInfo.EventHandlerType
              .GetMethod("Invoke")
              .GetParameters()
              .Select(parameter => Expression.Parameter(parameter.ParameterType))
              .ToArray();
        }

        private Delegate GetHandler(EventInfo eventInfo,
                    Action action, ParameterExpression[] parameters)
        {
            return Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action),
                          "Invoke", Type.EmptyTypes), parameters)
              .Compile();
        }

        private Delegate GetHandler(EventInfo eventInfo,
            Action<object, EventArgs> action, MethodInfo invoke, ParameterExpression[] parameters)
        {
            return Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action), invoke, parameters),
                parameters
              )
              .Compile();
        }
    }
}
