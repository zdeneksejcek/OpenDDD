namespace OpenDDD.RemoteQueue
{
    public interface IRemoteDequeue
    {
        Command DequeueCommand();

        Event DequeueEvent();
    }
}
