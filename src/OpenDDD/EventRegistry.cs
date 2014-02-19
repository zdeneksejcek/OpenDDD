using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenDDD
{
    public static class EventRegistry
    {
        private static List<TypeHandleTuple> Listeners = new List<TypeHandleTuple>();

        public static void Register<T>(NonTransactionalEventHandler<T> listener) where T : Event
        {
            var type = GetGenericType(listener);

            Listeners.Add(new TypeHandleTuple(type, @event => listener.HandleEvent(@event as T)));
        }

        private static Type GetGenericType(object obj)
        {
            var type = obj.GetType().BaseType;
            var arguments = type.GetGenericArguments();
            return arguments.FirstOrDefault();
        }

        private static IEnumerable<TypeHandleTuple> GetListenersByEventType(Type type)
        {
            return Listeners.Where(p => p.Type == type);
        }

        public static void Handle(Event @event)
        {
            foreach (var listener in GetListenersByEventType(@event.GetType()))
                listener.Action(@event);
        }

        internal class TypeHandleTuple
        {
            public Type Type { get; private set; }
            public Action<Event> Action { get; private set; }

            public TypeHandleTuple(Type type, Action<Event> action)
            {
                Type = type;
                Action = action;
            }
        }

    }
}