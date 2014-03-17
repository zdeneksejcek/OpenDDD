using System;
using System.Linq;
using System.Reflection;

namespace OpenDDD.Reflection
{
    public class InterfacesFinder
    {
        public static Type[] FindApplicationServices(Assembly[] assemblies)
        {
            var interfaceType = typeof (IApplicationService);

            var types = assemblies.SelectMany(s => s.GetTypes()).Where(p=>interfaceType.IsAssignableFrom(p) && p.IsClass);

            return types.ToArray();
        }

        public static Type[] FindExternalDependencies(Assembly[] assemblies)
        {
            var interfaceType = typeof(IExternalImplementationRequired);

            var interfaceTypes = assemblies.SelectMany(s => s.GetTypes()).Where(p => interfaceType.IsAssignableFrom(p) && p.IsInterface);

            return interfaceTypes.ToArray();
        }
    }
}
