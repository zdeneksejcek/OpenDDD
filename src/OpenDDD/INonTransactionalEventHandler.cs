
namespace OpenDDD
{
    public interface INonTransactionalEventHandler<in TEvent> : IEventHandler<TEvent> where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}