
namespace OpenDDD
{
    public abstract class NonTransactionalEventHandler<TEvent> where TEvent : Event
    {
        public abstract void HandleEvent(TEvent @event);
    }
}