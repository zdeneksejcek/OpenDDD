
using System;

namespace OpenDDD
{
    public interface ITypeInstantiator
    {
        T Instantiate<T>(Type type);
        object Instantiate(Type type);

        T Instantiate<T>();
    }
}