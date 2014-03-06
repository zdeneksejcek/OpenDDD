using System;
using System.Collections.Generic;
using System.Reflection;

namespace OpenDDD
{
    public abstract class BoundedContext
    {
        public virtual IEnumerable<Type> GetEventListeners()
        {
            return new List<Type>();
        }

        public virtual IEnumerable<Assembly> GetAssembliesForIoCRegistration()
        {
            return new List<Assembly>();
        } 
    }
}
