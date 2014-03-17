
namespace OpenDDD
{
    public interface IImmediateEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}