namespace OpenDDD.RemoteEventQueue
{
    public interface IRemoteEventQueue
    {
        void Enqueue(Event @event);
    }
}