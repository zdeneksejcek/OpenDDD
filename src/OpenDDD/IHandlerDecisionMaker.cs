using System;
namespace OpenDDD
{
    public interface IHandlerDecisionMaker
    {
        bool ShouldBeHandled(Type type);
    }
}
