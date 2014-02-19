using System;
using System.Collections.Generic;
using System.Reflection;

namespace OpenDDD
{
    public class EventFinder
    {
        public static IEnumerable<IEventListener> FindAllInAssembly(Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes)
                if (typeof (IEventListener).IsAssignableFrom(type))
                    yield return Activator.CreateInstance(type) as IEventListener;
        }
    }
}
