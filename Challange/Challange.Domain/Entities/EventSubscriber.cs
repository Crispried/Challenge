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
        public static void AddEventHandler(EventInfo eventInfo, object item, Action action)
        {
            var parameters = GetParameters(eventInfo);
            var handler = GetHandler(eventInfo, action, parameters);
            eventInfo.AddEventHandler(item, handler);
        }

        public static void AddEventHandler(EventInfo eventInfo, object item, Action<object, EventArgs> action)
        {
            var parameters = GetParameters(eventInfo);
            var invoke = GetMethod(action);
            var handler = GetHandler(eventInfo, action, invoke, parameters);
            eventInfo.AddEventHandler(item, handler);
        }

        public static void RemoveEventHandler(EventInfo eventInfo,
                            object item, Action action)
        {
            var parameters = GetParameters(eventInfo);
            var handler = GetHandler(eventInfo, action, parameters);
            eventInfo.RemoveEventHandler(item, handler);
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
