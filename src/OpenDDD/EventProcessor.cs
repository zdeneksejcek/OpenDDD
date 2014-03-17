using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenDDD.RemoteEventQueue;

namespace OpenDDD
{
    public class EventProcessor
    {
        private IRemoteEventQueue MessageQueue { get; set; }
        private Dictionary<Type, List<Type>> _eventualEventsWithHandlers;
        private Dictionary<Type, List<Type>> _immediateEventsWithHandlers;
        private IHandlerDecisionMaker _handlerDecisionMaker;
        private ITypeInstantiator _instantiator;

        public EventProcessor(IRemoteEventQueue queue, Type[] handlerTypes, IHandlerDecisionMaker handlerDecisionMaker, ITypeInstantiator instantiator)
        {
            MessageQueue = queue;
            _instantiator = instantiator;
            _eventualEventsWithHandlers = RegisterEvents(handlerTypes, typeof(IEventualEventHandler<>));
            _immediateEventsWithHandlers = RegisterEvents(handlerTypes, typeof(IImmediateEventHandler<>));

            _handlerDecisionMaker = handlerDecisionMaker;
            _instantiator = instantiator;
        }

        public void Process(Event @event)
        {
            var eventualHandlerTypes = GetEventualEventHandlersByEvent(@event.GetType());

            foreach (var handlerType in eventualHandlerTypes)
            {
                if (_handlerDecisionMaker.ShouldBeHandled(handlerType))
                    ExecuteEventHandler(handlerType, @event);
                else
                    MessageQueue.Enqueue(@event);
            }
        }

        private void ExecuteEventHandler(Type handlerType, Event @event)
        {
            var method = GetMethodOnHandler(handlerType, @event.GetType());
            var handlerInstance = _instantiator.Instantiate(handlerType);

            method.Invoke(handlerInstance, new object[] {@event});
        }

        private MethodInfo GetMethodOnHandler(Type handlerType, Type eventType)
        {
            var matchingMethods =
                handlerType.GetMethods()
                .Where(info => IsParameterMatching(info.GetParameters(), eventType)).ToArray();

            if (matchingMethods.Length == 1)
                return matchingMethods[0];
    
            throw new Exception("Too many or zero methods with exact one parameter of event type");
        }

        private static bool IsParameterMatching(ParameterInfo[] parameters, Type commandType)
        {
            if (parameters.Count() != 1) return false;

            var parameterType = parameters[0].ParameterType;

            return parameterType == commandType;
        }

        private Dictionary<Type, List<Type>> RegisterEvents(Type[] handlerTypes, Type genericInterface)
        {
            var handlers = new Dictionary<Type, List<Type>>();

            foreach (var type in handlerTypes)
            {
                var handledEventTypes = GetHandledEventsForHandler(type, genericInterface);

                foreach (var eventType in handledEventTypes)
                {
                    if (handlers.ContainsKey(eventType))
                        handlers[eventType].Add(type);
                    else
                    {
                        var list = new List<Type> { type };
                        handlers.Add(eventType, list);
                    }
                }
            }

            return handlers;
        }

        private Type[] GetHandledEventsForHandler(Type handler, Type genericInterface)
        {
            var handledEventTypes = new List<Type>();

            foreach (var @interface in handler.GetInterfaces())
            {
                var match = @interface.IsGenericType && @interface.GetGenericTypeDefinition() == genericInterface;

                if (match)
                    handledEventTypes.Add(@interface.GetGenericArguments()[0]);
            }

            return handledEventTypes.ToArray();
        }

        public Type[] GetEventualEventHandlersByEvent(Type eventType)
        {
            if (_eventualEventsWithHandlers.ContainsKey(eventType))
                return _eventualEventsWithHandlers[eventType].ToArray();

            return new Type[0];
        }

        public Type[] GetImmediateEventHandlersByEvent(Type eventType)
        {
            if (_immediateEventsWithHandlers.ContainsKey(eventType))
                return _eventualEventsWithHandlers[eventType].ToArray();

            return new Type[0];
        }


    }
}