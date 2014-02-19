using System.Collections.Generic;
using System.Reflection;

namespace OpenDDD
{
    public abstract class BoundedContext
    {
        public virtual IEnumerable<IEventListener> GetEventListeners()
        {
            return new List<IEventListener>();
        }

        public virtual IEnumerable<Assembly> GetAssembliesForIoCRegistration()
        {
            return new List<Assembly>();
        } 
    }
}
