namespace OpenDDD
{
    public interface ITransactionalEventHandler<in TEvent> : IEventHandler<TEvent> where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}