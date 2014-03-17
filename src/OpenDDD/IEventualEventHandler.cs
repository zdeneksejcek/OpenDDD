namespace OpenDDD
{
    public interface IEventualEventHandler<in TEvent> : IEventHandler where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}