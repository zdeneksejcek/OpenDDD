using System.Reflection;

namespace OpenDDD
{
    public interface IDomainAssemblyProvider
    {
        Assembly[] GetDomainAssemblies();
    }
}