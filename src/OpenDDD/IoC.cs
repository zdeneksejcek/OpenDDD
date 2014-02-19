
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OpenDDD
{
    internal class IoC
    {
        private Func<Type, object> CallbackFunction { get; set; }

        private List<Assembly> _assembliesForRegistration = new List<Assembly>();

        public IEnumerable<Assembly> AssembliesForRegistration { get { return _assembliesForRegistration; } }

        public void AddAssemblyForRegistration(Assembly assembly)
        {
            _assembliesForRegistration.Add(assembly);
        }

        public void RegisterCallback(Func<Type, object> callback)
        {
            CallbackFunction = callback;
        }

        public T Resolve<T>() where T : class 
        {
            if (CallbackFunction == null)
                throw new IoCCallbackFunctionIsNotRegistered();

            return CallbackFunction(typeof (T)) as T;
        }



        public class IoCCallbackFunctionIsNotRegistered : Exception { }
    }
}
