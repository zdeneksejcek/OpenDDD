
using System;
using OpenDDD.Messaging;

namespace OpenDDD
{
    public static class Application
    {
        private static IoC IoC = new IoC();

        public static void RegisterBoundedContext(params BoundedContext[] contexts)
        {
            RegisterContexts(contexts);
        }

        private static void RegisterContexts(BoundedContext[] contexts)
        {
            foreach (var context in contexts)
            {
                foreach (var listener in context.GetEventListeners()) ;
                    //EventRegistry.Register();

                foreach (var assembly in context.GetAssembliesForIoCRegistration())
                    IoC.AddAssemblyForRegistration(assembly);
            }
        }

        public static void RegisterIoCCallback(Func<Type, object> callback)
        {
            IoC.RegisterCallback(callback);
        }

        public static void Use(IMessageQueue messageQueue)
        {
            
        }

        public static void Start()
        {
            
        }
    }
}