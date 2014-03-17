using System;
using System.Linq;
using System.Reflection;

namespace OpenDDD.Reflection
{
    public class MethodFinder
    {
        private Type[] _types;

        public MethodFinder(Type[] types)
        {
            _types = types;
        }

        public MethodInfo FindByCommandAndReturnType<TCommand, TReturn>()
        {
            var returnType = typeof (TReturn);
            var commandType = typeof (TCommand);

            var matchingMethods =
                _types
                .SelectMany(p => p.GetMethods())
                .Where(info => info.ReturnType == returnType && IsParameterMatching(info.GetParameters(), commandType)).ToArray();

            if (matchingMethods.Length == 1)
                return matchingMethods[0];

            throw new Exception();
        }

        public MethodInfo FindByCommand<TCommand>()
        {
            var commandType = typeof(TCommand);

            var matchingMethods =
                _types
                .SelectMany(p => p.GetMethods())
                .Where(info => IsParameterMatching(info.GetParameters(), commandType)).ToArray();

            if (matchingMethods.Length == 1)
                return matchingMethods[0];

            throw new Exception();
        }

        private static bool IsParameterMatching(ParameterInfo[] parameters, Type commandType)
        {
            if (parameters.Count() != 1) return false;

            var parameterType = parameters[0].ParameterType;

            return parameterType == commandType;
        }
    }
}
