using System;
using System.Linq;
using System.Reflection;

namespace OpenDDD.Reflection
{
    public class EventHandlerFinder
    {
        public static Type[] FindEventHandlers(Assembly[] assemblies)
        {
            var interfaceType = typeof(IEventHandler);

            var interfaceTypes = assemblies.SelectMany(s => s.GetTypes()).Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass);

            return interfaceTypes.ToArray();
        }
    }
}
