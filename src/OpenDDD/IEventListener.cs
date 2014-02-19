namespace OpenDDD
{
    public interface IEventListener
    {
        bool CanHandle(Event @event);

        void HandleEvent(Event @event);
    }
}
