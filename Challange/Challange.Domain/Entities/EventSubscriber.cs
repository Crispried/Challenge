using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Challange.Domain.Entities
{
    public static class EventSubscriber
    {
        public static Delegate AddEventHandler(object item, 
                            string eventName, Action action)
        {
            var eventInfo = GetEventInfo(item.GetType(), eventName);
            var parameters = GetParameters(eventInfo);
            var handler = GetHandler(eventInfo, action, parameters);
            eventInfo.AddEventHandler(item, handler);
            return handler;
        }

        public static Delegate AddEventHandler(object item,
                    string eventName, Action<object, EventArgs> action)
        {
            var eventInfo = GetEventInfo(item.GetType(), eventName);
            var parameters = GetParameters(eventInfo);
            var invoke = GetMethod(action);
            var handler = GetHandler(eventInfo, action, invoke, parameters);
            eventInfo.AddEventHandler(item, handler);
            return handler;
        }

        public static void RemoveEventHandler(object item, string eventName,
                                        Delegate handler)
        {
            var eventInfo = GetEventInfo(item.GetType(), eventName);
            eventInfo.RemoveEventHandler(item, handler);
        }

        private static EventInfo GetEventInfo(Type type, string eventName)
        {
            return type.GetEvent("Elapsed");
        }

        private static ParameterExpression[] GetParameters(EventInfo eventInfo)
        {
            return eventInfo.EventHandlerType
              .GetMethod("Invoke")
              .GetParameters()
              .Select(parameter => Expression.Parameter(parameter.ParameterType))
              .ToArray();
        }

        private static Delegate GetHandler(EventInfo eventInfo,
                    Action action, ParameterExpression[] parameters)
        {
            return Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action),
                          "Invoke", Type.EmptyTypes), parameters)
              .Compile();
        }

        private static Delegate GetHandler(EventInfo eventInfo,
            Action<object, EventArgs> action, MethodInfo methodInfo,
            ParameterExpression[] parameters)
        {
            return Expression.Lambda(
                eventInfo.EventHandlerType,
                Expression.Call(Expression.Constant(action),
                methodInfo, parameters), parameters)
              .Compile();
        }

        private static MethodInfo GetMethod(Action<object, EventArgs> action)
        {
            return action.GetType().GetMethod("Invoke");
        }
    }
}
