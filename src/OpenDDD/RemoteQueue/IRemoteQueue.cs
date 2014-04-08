namespace OpenDDD.RemoteQueue
{
    public interface IRemoteQueue
    {
        void Enqueue(Event @event);

        void Enqueue(Event[] events);

        void Enqueue(Command command);

        void Enqueue(Command[] command);
    }
}